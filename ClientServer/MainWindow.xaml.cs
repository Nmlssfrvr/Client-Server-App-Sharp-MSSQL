using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Data.SqlClient;

namespace ClientServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int UserId;
        public static string UserName;
        public static string SqlServer;
        public static bool admin;
        public static string GeneratePassHash(string pass)
        {
            //Шифрование
            return pass;
        }
        public static List<string> Rcodes = new List<string>() {"1234567890","0987654321"};
        public MainWindow()
        {
            InitializeComponent();
            RegisterGrid.Visibility = Visibility.Collapsed;
            MainGrid.Visibility = Visibility.Collapsed;
            ResizeMode = ResizeMode.NoResize;
            using (UstinovContext db = new UstinovContext())
                db.UserData.Load();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Notation.Text = "";
            using (UstinovContext db = new UstinovContext())
            {
                admin = false;
                if (string.IsNullOrWhiteSpace(Login.Text) || string.IsNullOrWhiteSpace(Password.Password))
                {
                    Notation.Foreground = Brushes.Red;
                    Notation.Text = "Оба поля должны быть заполнены";
                    return;
                }
                if (Login.Text.Length > 40)
                {
                    Notation.Foreground = Brushes.Red;
                    Notation.Text = "Логин должен быть от 1 до 40 символов";
                    return;
                }
                var exist = db.UserData.Select(u => UstinovContext.ValidUser(Login.Text, Password.Password)).FirstOrDefault();
                if (exist == 1)
                {
                    SqlServer = ServerName.Text.Replace(" ",""); 
                    Notation.Text = "Успешный вход";
                    var user = db.UserData.Where(u => u.ULogin == Login.Text)
                                          .Join(db.Clients, u => u.UInfo, c => c.CId, (u, c) => new { Name = c.CFn, Id = c.CId })
                                          .FirstOrDefault();
                    Greeting.Content = "Здравствуйте, " + user.Name;
                    UserId = user.Id;
                    UserName = user.Name;
                    var roles = db.UserData.Join(db.UserGroups, ud => ud.URole, ug => ug.UgGroup, (ud, ug) => new { ud, ug })
                                           .Join(db.Roles, comb => comb.ug.UgRole, id => id.RId, (comb, id) => new { comb, id })
                                           .Join(db.Clients, info => info.comb.ud.UInfo, cl => cl.CId, (info, cl) => new { info, cl })
                                           .Where(id => id.cl.CId == user.Id).ToList();
                    LoginGrid.Visibility = Visibility.Collapsed;
                    ShowParcels.Visibility = Visibility.Collapsed;
                    WorkerPanel.Visibility = Visibility.Collapsed;
                    AdminPanel.Visibility = Visibility.Collapsed;
                    if (roles.Any(role => role.info.id.RRole == "reciever"))
                        ShowParcels.Visibility = Visibility.Visible;
                    if (roles.Any(role => role.info.id.RRole == "worker"))
                        WorkerPanel.Visibility = Visibility.Visible;
                    if (roles.Any(role => role.info.id.RRole == "admin"))
                    {
                        AdminPanel.Visibility = Visibility.Visible;
                        admin = true;
                    }
                        
                    MainGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    Notation.Foreground = Brushes.Red;
                    Notation.Text = "Неверный логин или пароль";
                }
            }
                
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            LoginGrid.Visibility = Visibility.Collapsed;
            RegisterGrid.Visibility = Visibility.Visible;
            RFullName.Text = "";
            RPSeries.Text = "";
            RPNumber.Text = "";
            RLogin.Text = "";
            RPass.Text = "";
            RCode.Text = "";
            List<decimal> indexes = new List<decimal>();
            using (UstinovContext db = new UstinovContext())
            {
                indexes = db.MailPosts.Select(u => u.MIndex).ToList();
            }
            RIndex.ItemsSource = indexes;
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RFullName.Text) || string.IsNullOrWhiteSpace(RPSeries.Text) ||
                string.IsNullOrWhiteSpace(RPNumber.Text) || string.IsNullOrWhiteSpace(RLogin.Text) ||
                string.IsNullOrWhiteSpace(RPass.Text))
            {
                MessageBox.Show("Не все поля заполнены");
                return;
            }
            Regex pass = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{5,}$");
            if (!pass.IsMatch(RPass.Text))
            {
                MessageBox.Show("Пароль должен быть минимум 5 символов в длину, состоять из латинских букв, а также содержать хотя бы одну цифру");
                return;
            }
            if (RIndex.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали индекс");
                return;
            }
            using (SqlConnection connection = new SqlConnection($"Data Source={ServerName.Text.Replace(" ","")};Initial Catalog=Ustinov; Integrated Security =True"))
            {
                connection.Open();
                SqlCommand CreateClient = new SqlCommand("P_Client_create",connection);
                CreateClient.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter NewIndex = new SqlParameter
                {
                    ParameterName = "@new_index",
                    SqlDbType = System.Data.SqlDbType.Decimal,
                    Value = RIndex.SelectedItem
                };
                SqlParameter NewFN = new SqlParameter
                {
                    ParameterName = "@new_FN",
                    Value = RFullName.Text
                };
                SqlParameter NewPSeries = new SqlParameter
                {
                    ParameterName = "@new_passport_series",
                    Value = Convert.ToInt16(RPSeries.Text)
                };
                SqlParameter NewPNumber = new SqlParameter
                {
                    ParameterName = "@new_passport_number",
                    Value = Convert.ToInt32(RPNumber.Text)
                };
                CreateClient.Parameters.Add(NewIndex);
                CreateClient.Parameters.Add(NewFN);
                CreateClient.Parameters.Add(NewPSeries);
                CreateClient.Parameters.Add(NewPNumber);
                var resultid = CreateClient.ExecuteScalar();
                SqlCommand CreateUserData = new SqlCommand("P_UserData_Create", connection);
                CreateUserData.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter NewInfo = new SqlParameter
                {
                    ParameterName = "@new_info",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = Convert.ToInt32(resultid)
                };
                SqlParameter NewLogin = new SqlParameter
                {
                    ParameterName = "@new_login",
                    Value = RLogin.Text
                };
                SqlParameter NewPass = new SqlParameter
                {
                    ParameterName = "@new_pass",
                    Value = GeneratePassHash(RPass.Text)
                };
                int role = 1;
                if (Rcodes[0] == RCode.Text)
                    role = 2;
                else if (Rcodes[1] == RCode.Text)
                    role = 3;
                SqlParameter NewRole = new SqlParameter
                {
                    ParameterName = "@new_role",
                    Value = role
                };
                CreateUserData.Parameters.Add(NewInfo);
                CreateUserData.Parameters.Add(NewLogin);
                CreateUserData.Parameters.Add(NewPass);
                CreateUserData.Parameters.Add(NewRole);
                var result = CreateUserData.ExecuteNonQuery();
            }
            Notation.Foreground = Brushes.Green;
            Notation.Text = "Теперь вам нужно войти";
            Login.Text = "";
            Password.Password = "";
            RegisterGrid.Visibility = Visibility.Collapsed;
            LoginGrid.Visibility = Visibility.Visible;
        }

        private void BackToLogin_Click(object sender, RoutedEventArgs e)
        {
            RegisterGrid.Visibility = Visibility.Collapsed;
            LoginGrid.Visibility = Visibility.Visible;
        }

        private void ShowParcels_Click(object sender, RoutedEventArgs e)
        {
            ParcelsWindow window = new ParcelsWindow();
            window.Title = "Посылки пользователя " + UserName;
            window.ShowDialog();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Visibility = Visibility.Collapsed;
            Notation.Text = "";
            Login.Text = "";
            Password.Password = "";
            LoginGrid.Visibility = Visibility.Visible;
        }

        private void WorkerPanel_Click(object sender, RoutedEventArgs e)
        {
            WorkerWindow window = new WorkerWindow();
            window.Title = "Панель работника";
            window.ShowDialog();
        }

        private void AdminPanel_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow window = new AdminWindow();
            window.Title = "Панель администратора";
            window.ShowDialog();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClientServer
{
    /// <summary>
    /// Логика взаимодействия для TableClientsWindow.xaml
    /// </summary>
    public partial class TableClientsWindow : Window
    {
        public TableClientsWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            using (SqlConnection connection = new SqlConnection($"Data Source={MainWindow.SqlServer};Initial Catalog=Ustinov; Integrated Security =True"))
            {
                connection.Open();
                string query = $"SELECT * FROM Client";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                TableClientsGrid.ItemsSource = ds.Tables[0].DefaultView;
                TableClientsGrid.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                TableClientsGrid.CanUserAddRows = false;
                TableClientsGrid.CanUserDeleteRows = false;
            }
            EditClient.IsEnabled = false;
            DeleteClient.IsEnabled = false;
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            AddEditClientWindow window = new AddEditClientWindow();
            using (UstinovContext db = new UstinovContext())
            {
                var indexes = db.MailPosts.Select(m => new { m.MIndex, m.MAddress }).ToList();
                window.NewIndex.ItemsSource = indexes.ConvertAll(el => el.MIndex.ToString() + " " + el.MAddress);
            }
            window.Title = "Добавление клиента";
            window.Edit.Visibility = Visibility.Collapsed;
            window.Add.Visibility = Visibility.Visible;
            window.ShowDialog();
        }

        private void EditClient_Click(object sender, RoutedEventArgs e)
        {
            AddEditClientWindow window = new AddEditClientWindow();
            using (UstinovContext db = new UstinovContext())
            {
                var indexes = db.MailPosts.Select(m => new { m.MIndex, m.MAddress }).ToList();
                window.NewIndex.ItemsSource = indexes.ConvertAll(el => el.MIndex.ToString() + " " + el.MAddress);
                DataRowView row = (DataRowView)TableClientsGrid.SelectedItems[0];
                window.NewIndex.SelectedItem = indexes.ConvertAll(el => el.MIndex.ToString() + " " + el.MAddress).Find(el => el.Split(" ")[0] == row[1].ToString());
                window.NewFN.Text = row[2].ToString();
                window.NewPSeries.Text = row[3].ToString();
                window.NewPNumber.Text = row[4].ToString();
                window.IdLabel.Content = "Id " + row[0].ToString();
            }
            window.Title = "Изменение данных о клиенте";
            window.Add.Visibility = Visibility.Collapsed;
            window.Edit.Visibility = Visibility.Visible;
            window.ShowDialog();
        }

        private void DeleteClient_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection($"Data Source={MainWindow.SqlServer};Initial Catalog=Ustinov; Integrated Security =True"))
            {
                connection.Open();
                DataRowView row = (DataRowView)TableClientsGrid.SelectedItems[0];
                SqlCommand Delete = new SqlCommand("P_Client_Delete", connection);
                Delete.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter Id = new SqlParameter
                {
                    ParameterName = "@id",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = Convert.ToInt32(row[0])
                };
                Delete.Parameters.Add(Id);
                var deletedid = Delete.ExecuteNonQuery();
                string query = $"SELECT * FROM Client";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                TableClientsGrid.ItemsSource = ds.Tables[0].DefaultView;
            }
            EditClient.IsEnabled = false;
            DeleteClient.IsEnabled = false;
            TableClientsGrid.SelectedItem = null;
        }

        private void TableClientsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditClient.IsEnabled = true;
            DeleteClient.IsEnabled = true;
        }

        private void UpdateGrid_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection($"Data Source={MainWindow.SqlServer};Initial Catalog=Ustinov; Integrated Security =True"))
            {
                connection.Open();
                string query = $"SELECT * FROM Client";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                TableClientsGrid.ItemsSource = ds.Tables[0].DefaultView;
            }
            EditClient.IsEnabled = false;
            DeleteClient.IsEnabled = false;
            TableClientsGrid.SelectedItem = null;
        }
    }
}

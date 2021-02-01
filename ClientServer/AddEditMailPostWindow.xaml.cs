using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для AddEditMailPostWindow.xaml
    /// </summary>
    public partial class AddEditMailPostWindow : Window
    {
        public AddEditMailPostWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NewAddress.Text) || string.IsNullOrWhiteSpace(NewPostamatCount.Text) || string.IsNullOrWhiteSpace(NewIndex.Text) || string.IsNullOrWhiteSpace(NewPhone.Text))
            {
                MessageBox.Show("Не все поля заполнены");
                return;
            }
            if (NewPhone.Text[0] != '7' || NewPhone.Text.Length != 11)
            {
                MessageBox.Show("Телефон начинается с цифры '7' и имеет 11 цифр");
                return;
            }
            using (UstinovContext db = new UstinovContext())
            {
                var indexes = db.MailPosts.Select(i => i.MIndex).ToList();
                if (indexes.Any(i => i == Convert.ToDecimal(NewIndex.Text)))
                {
                    MessageBox.Show("Такой индекс уже есть в базе данных");
                    return;
                }
            }
            using (SqlConnection connection = new SqlConnection($"Data Source={MainWindow.SqlServer};Initial Catalog=Ustinov; Integrated Security =True"))
            {
                connection.Open();
                SqlCommand Create = new SqlCommand("P_MailPost_Create", connection);
                Create.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter NIndex = new SqlParameter
                {
                    ParameterName = "@new_index",
                    SqlDbType = System.Data.SqlDbType.Decimal,
                    Value = Convert.ToDecimal(NewIndex.Text)
                };
                SqlParameter NAddress = new SqlParameter
                {
                    ParameterName = "@new_address",
                    Value = NewAddress.Text
                };
                SqlParameter NPhone = new SqlParameter
                {
                    ParameterName = "@new_phone",
                    SqlDbType = System.Data.SqlDbType.Decimal,
                    Value = NewPhone.Text
                };
                SqlParameter NPCount = new SqlParameter
                {
                    ParameterName = "@new_postamat_count",
                    Value = Convert.ToByte(NewPostamatCount.Text)
                };
                Create.Parameters.Add(NIndex);
                Create.Parameters.Add(NAddress);
                Create.Parameters.Add(NPhone);
                Create.Parameters.Add(NPCount);
                var resultid = Create.ExecuteNonQuery();
            }
            this.Close();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NewAddress.Text) || string.IsNullOrWhiteSpace(NewPostamatCount.Text) || string.IsNullOrWhiteSpace(NewIndex.Text) || string.IsNullOrWhiteSpace(NewPhone.Text))
            {
                MessageBox.Show("Не все поля заполнены");
                return;
            }
            if (NewPhone.Text[0] != '7' || NewPhone.Text.Length != 11)
            {
                MessageBox.Show("Телефон начинается с цифры '7' и имеет 11 цифр");
                return;
            }
            using (UstinovContext db = new UstinovContext())
            {
                var idindex = db.MailPosts.Select(i => new { i.MId, i.MIndex }).ToList();
                if (idindex.Any(i => i.MId != Convert.ToInt32(IdLabel.Content.ToString().Remove(0, 3)) && i.MIndex == Convert.ToDecimal(NewIndex.Text)))
                {
                    MessageBox.Show("Такой индекс уже есть в базе данных");
                    return;
                }
            }
            using (SqlConnection connection = new SqlConnection($"Data Source={MainWindow.SqlServer};Initial Catalog=Ustinov; Integrated Security =True"))
            {
                connection.Open();
                SqlCommand Edit = new SqlCommand("P_MailPost_Update", connection);
                Edit.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter Id = new SqlParameter
                {
                    ParameterName = "@id",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = Convert.ToInt32(IdLabel.Content.ToString().Remove(0, 3))
                };
                SqlParameter NIndex = new SqlParameter
                {
                    ParameterName = "@new_index",
                    SqlDbType = System.Data.SqlDbType.Decimal,
                    Value = NewIndex.Text
                };
                SqlParameter NAddress = new SqlParameter
                {
                    ParameterName = "@new_address",
                    Value = NewAddress.Text
                };
                SqlParameter NPhone = new SqlParameter
                {
                    ParameterName = "@new_phone",
                    SqlDbType = System.Data.SqlDbType.Decimal,
                    Value = NewPhone.Text
                };
                SqlParameter NPCount = new SqlParameter
                {
                    ParameterName = "@new_postamat_count",
                    Value = Convert.ToByte(NewPostamatCount.Text)
                };
                Edit.Parameters.Add(Id);
                Edit.Parameters.Add(NIndex);
                Edit.Parameters.Add(NAddress);
                Edit.Parameters.Add(NPhone);
                Edit.Parameters.Add(NPCount);
                var resultid = Edit.ExecuteNonQuery();
            }
            this.Close();
        }
    }
}

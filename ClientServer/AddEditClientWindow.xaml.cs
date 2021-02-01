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
    /// Логика взаимодействия для AddEditClientWindow.xaml
    /// </summary>
    public partial class AddEditClientWindow : Window
    {
        public AddEditClientWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            int value;
            if (string.IsNullOrWhiteSpace(NewFN.Text) || NewIndex.SelectedIndex == -1)
            {
                MessageBox.Show("Не все поля заполнены");
                return;
            }
            if (NewPSeries.Text.Length != 4 || !Int32.TryParse(NewPSeries.Text, out value))
            {
                MessageBox.Show("Серия паспорта должна иметь 4 цифры");
                return;
            }
            if (NewPNumber.Text.Length != 6 || !Int32.TryParse(NewPNumber.Text, out value))
            {
                MessageBox.Show("Номер паспорта должен иметь 6 цифр");
                return;
            }
            using (SqlConnection connection = new SqlConnection($"Data Source={MainWindow.SqlServer};Initial Catalog=Ustinov; Integrated Security =True"))
            {
                connection.Open();
                SqlCommand Create = new SqlCommand("P_Client_Create", connection);
                Create.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter NIndex = new SqlParameter
                {
                    ParameterName = "@new_index",
                    SqlDbType = System.Data.SqlDbType.Decimal,
                    Value = Convert.ToDecimal(NewIndex.Text.Split(" ")[0])
                };
                SqlParameter NFN = new SqlParameter
                {
                    ParameterName = "@new_FN",
                    Value = NewFN.Text
                };
                SqlParameter NPSeries = new SqlParameter
                {
                    ParameterName = "@new_passport_series",
                    Value = Convert.ToInt16(NewPSeries.Text)
                };
                SqlParameter NPNumber = new SqlParameter
                {
                    ParameterName = "@new_passport_number",
                    Value = Convert.ToInt32(NewPNumber.Text)
                };
                Create.Parameters.Add(NIndex);
                Create.Parameters.Add(NFN);
                Create.Parameters.Add(NPSeries);
                Create.Parameters.Add(NPNumber);
                var resultid = Create.ExecuteNonQuery();
            }
            this.Close();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            int value;
            if (string.IsNullOrWhiteSpace(NewFN.Text) || NewIndex.SelectedIndex == -1)
            {
                MessageBox.Show("Не все поля заполнены");
                return;
            }
            if (NewPSeries.Text.Length != 4 || !Int32.TryParse(NewPSeries.Text, out value))
            {
                MessageBox.Show("Серия паспорта должна иметь 4 цифры");
                return;
            }
            if (NewPNumber.Text.Length != 6 || !Int32.TryParse(NewPNumber.Text, out value))
            {
                MessageBox.Show("Номер паспорта должен иметь 6 цифр");
                return;
            }
            using (SqlConnection connection = new SqlConnection($"Data Source={MainWindow.SqlServer};Initial Catalog=Ustinov; Integrated Security =True"))
            {
                connection.Open();
                SqlCommand Edit = new SqlCommand("P_Client_Update", connection);
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
                    Value = Convert.ToDecimal(NewIndex.Text.Split(" ")[0])
                };
                SqlParameter NFN = new SqlParameter
                {
                    ParameterName = "@new_FN",
                    Value = NewFN.Text
                };
                SqlParameter NPSeries = new SqlParameter
                {
                    ParameterName = "@new_passport_series",
                    Value = Convert.ToInt16(NewPSeries.Text)
                };
                SqlParameter NPNumber = new SqlParameter
                {
                    ParameterName = "@new_passport_number",
                    Value = Convert.ToInt32(NewPNumber.Text)
                };
                Edit.Parameters.Add(Id);
                Edit.Parameters.Add(NIndex);
                Edit.Parameters.Add(NFN);
                Edit.Parameters.Add(NPSeries);
                Edit.Parameters.Add(NPNumber);
                var resultid = Edit.ExecuteNonQuery();
            }
            this.Close();
        }
    }
}

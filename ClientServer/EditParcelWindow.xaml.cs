using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для EditParcelWindow.xaml
    /// </summary>
    public partial class EditParcelWindow : Window
    {
        public EditParcelWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (NewReciever.SelectedIndex == -1 || NewInventory.SelectedIndex == -1 || NewOffice.SelectedIndex == -1 || NewTariff.SelectedIndex == -1 || string.IsNullOrWhiteSpace(NewTax.Text))
            {
                MessageBox.Show("Не все поля заполнены");
                return;
            }
            double dvalue;
            if (!double.TryParse(NewTax.Text, out dvalue))
            {
                MessageBox.Show("Поле 'налог' должно содержать число с плавающей точкой");
                return;
            }
            using (SqlConnection connection = new SqlConnection($"Data Source={MainWindow.SqlServer};Initial Catalog=Ustinov; Integrated Security =True"))
            {
                connection.Open();
                SqlCommand Edit = new SqlCommand("P_Parcel_Update", connection);
                Edit.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter Id = new SqlParameter
                {
                    ParameterName = "@id",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = Convert.ToInt32(IdLabel.Content.ToString().Remove(0,3))
                };
                SqlParameter NTax = new SqlParameter
                {
                    ParameterName = "@new_tax",
                    SqlDbType = System.Data.SqlDbType.Money,
                    Value = Convert.ToDouble(NewTax.Text)
                };
                SqlParameter NReciever = new SqlParameter
                {
                    ParameterName = "@new_reciever",
                    Value = Convert.ToInt32(NewReciever.SelectedValue.ToString().Split(" ")[0])
                };
                SqlParameter NInventory = new SqlParameter
                {
                    ParameterName = "@new_inventory",
                    Value = Convert.ToInt32(NewInventory.SelectedValue.ToString().Split(" ")[0])
                };
                SqlParameter NOffice = new SqlParameter
                {
                    ParameterName = "@new_office",
                    Value = Convert.ToInt32(NewOffice.SelectedValue.ToString().Split(" ")[0])
                };
                SqlParameter NTariff = new SqlParameter
                {
                    ParameterName = "@new_tariff",
                    Value = NewTariff.SelectedValue.ToString()
                };
                Edit.Parameters.Add(Id);
                Edit.Parameters.Add(NTax);
                Edit.Parameters.Add(NReciever);
                Edit.Parameters.Add(NInventory);
                Edit.Parameters.Add(NOffice);
                Edit.Parameters.Add(NTariff);
                var resultid = Edit.ExecuteNonQuery();
            }
            this.Close();
        }
    }
}

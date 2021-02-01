using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
    /// Логика взаимодействия для AddParcelWindow.xaml
    /// </summary>
    public partial class AddParcelWindow : Window
    {
        public AddParcelWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (NewReciever.SelectedIndex == -1 || NewInventory.SelectedIndex == -1 || NewOffice.SelectedIndex == -1 || NewTariff.SelectedIndex == -1 || string.IsNullOrWhiteSpace(NewTax.Text) )
            {
                MessageBox.Show("Не все поля заполнены");
                return;
            }
            double dvalue;
            if (!double.TryParse(NewTax.Text,out dvalue))
            {
                MessageBox.Show("Поле 'налог' должно содержать число с плавающей точкой");
                return;
            }
            using (SqlConnection connection = new SqlConnection($"Data Source={MainWindow.SqlServer};Initial Catalog=Ustinov; Integrated Security =True"))
            {
                connection.Open();
                SqlCommand Create = new SqlCommand("P_Parcel_Create", connection);
                Create.CommandType = System.Data.CommandType.StoredProcedure;
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
                Create.Parameters.Add(NTax);
                Create.Parameters.Add(NReciever);
                Create.Parameters.Add(NInventory);
                Create.Parameters.Add(NOffice);
                Create.Parameters.Add(NTariff);
                var resultid = Create.ExecuteNonQuery();
            }
            this.Close();
        }
    }
}

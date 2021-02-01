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
    /// Логика взаимодействия для AddInventoryWindow.xaml
    /// </summary>
    public partial class AddInventoryWindow : Window
    {
        public AddInventoryWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            int ivalue;
            double dvalue;
            if (string.IsNullOrWhiteSpace(NewProductName.Text) || !int.TryParse(NewAmount.Text, out ivalue) || !double.TryParse(NewPrice.Text, out dvalue) || NewFragile.SelectedIndex == -1)
            {
                MessageBox.Show("Не все поля заполнены верно. Строка 'имя продукта' не должна быть пустой, в строке 'количество' должно быть целое число, а в 'цена' - с плавающей точкой");
                return;
            }
            using (SqlConnection connection = new SqlConnection($"Data Source={MainWindow.SqlServer};Initial Catalog=Ustinov; Integrated Security =True"))
            {
                connection.Open();
                SqlCommand Create = new SqlCommand("P_Inventory_Create", connection);
                Create.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter NPName = new SqlParameter
                {
                    ParameterName = "@new_product_name",
                    Value = NewProductName.Text
                };
                SqlParameter NAmount = new SqlParameter
                {
                    ParameterName = "@new_amount",
                    Value = Convert.ToInt32(NewAmount.Text)
                };
                SqlParameter NPrice = new SqlParameter
                {
                    ParameterName = "@new_price",
                    SqlDbType = System.Data.SqlDbType.Money,
                    Value = Convert.ToDouble(NewPrice.Text)
                };
                SqlParameter NFragile = new SqlParameter
                {
                    ParameterName = "@new_fragile",
                    Value = NewFragile.SelectedValue.ToString()
                };
                Create.Parameters.Add(NPName);
                Create.Parameters.Add(NAmount);
                Create.Parameters.Add(NPrice);
                Create.Parameters.Add(NFragile);
                var resultid = Create.ExecuteNonQuery();
            }
            this.Close();
        }
    }
}

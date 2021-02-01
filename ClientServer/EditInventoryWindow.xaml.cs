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
    /// Логика взаимодействия для EditInventoryWindow.xaml
    /// </summary>
    public partial class EditInventoryWindow : Window
    {
        public EditInventoryWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
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
                SqlCommand Edit = new SqlCommand("P_Inventory_Update", connection);
                Edit.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter Id = new SqlParameter
                {
                    ParameterName = "@id",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = Convert.ToInt32(IdLabel.Content.ToString().Remove(0, 3))
                };
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
                Edit.Parameters.Add(Id);
                Edit.Parameters.Add(NPName);
                Edit.Parameters.Add(NAmount);
                Edit.Parameters.Add(NPrice);
                Edit.Parameters.Add(NFragile);
                var resultid = Edit.ExecuteNonQuery();
            }
            this.Close();
        }

    }
}

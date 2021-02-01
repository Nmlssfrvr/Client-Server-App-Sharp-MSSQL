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
    /// Логика взаимодействия для TableInventoriesWindow.xaml
    /// </summary>
    public partial class TableInventoriesWindow : Window
    {
        public TableInventoriesWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;

            using (SqlConnection connection = new SqlConnection($"Data Source={MainWindow.SqlServer};Initial Catalog=Ustinov; Integrated Security =True"))
            {
                connection.Open();
                string query = $"SELECT * FROM Inventory";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                TableInventoriesGrid.ItemsSource = ds.Tables[0].DefaultView;
                TableInventoriesGrid.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                TableInventoriesGrid.CanUserAddRows = false;
                TableInventoriesGrid.CanUserDeleteRows = false;
            }
            EditInventory.IsEnabled = false;
            DeleteInventory.IsEnabled = false;
        }

        private void AddInventory_Click(object sender, RoutedEventArgs e)
        {
            var fragile = new List<string>() { "yes", "no" };
            AddInventoryWindow window = new AddInventoryWindow();
            window.NewFragile.ItemsSource = fragile;
            window.Title = "Добавление новой описи";
            window.ShowDialog();
        }

        private void EditInventory_Click(object sender, RoutedEventArgs e)
        {
            var fragile = new List<string>() { "yes", "no" };
            EditInventoryWindow window = new EditInventoryWindow();
            window.NewFragile.ItemsSource = fragile;
            DataRowView row = (DataRowView)TableInventoriesGrid.SelectedItems[0];
            window.NewProductName.Text = row[1].ToString();
            window.NewAmount.Text = row[2].ToString();
            window.NewPrice.Text = row[3].ToString();
            window.NewFragile.SelectedItem = fragile.Find(el => el == row[4].ToString());
            window.IdLabel.Content = "Id " + row[0].ToString();
            window.Title = "Изменение информации об имеющейся описи";
            window.ShowDialog();
        }

        private void DeleteInventory_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection($"Data Source={MainWindow.SqlServer};Initial Catalog=Ustinov; Integrated Security =True"))
            {
                connection.Open();
                DataRowView row = (DataRowView)TableInventoriesGrid.SelectedItems[0];
                SqlCommand Delete = new SqlCommand("P_Inventory_Delete", connection);
                Delete.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter Id = new SqlParameter
                {
                    ParameterName = "@id",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = Convert.ToInt32(row[0])
                };
                Delete.Parameters.Add(Id);
                var deletedid = Delete.ExecuteNonQuery();
                string query = $"SELECT * FROM Inventory";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                TableInventoriesGrid.ItemsSource = ds.Tables[0].DefaultView;
            }
            EditInventory.IsEnabled = false;
            DeleteInventory.IsEnabled = false;
            TableInventoriesGrid.SelectedItem = null;
        }
        private void UpdateGrid_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection($"Data Source={MainWindow.SqlServer};Initial Catalog=Ustinov; Integrated Security =True"))
            {
                connection.Open();
                string query = $"SELECT * FROM Inventory";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                TableInventoriesGrid.ItemsSource = ds.Tables[0].DefaultView;
            }
            EditInventory.IsEnabled = false;
            DeleteInventory.IsEnabled = false;
            TableInventoriesGrid.SelectedItem = null;
        }

        private void TableInventoriesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditInventory.IsEnabled = true;
            DeleteInventory.IsEnabled = true;
        }
    }
}

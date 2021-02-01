using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            SolvingGrid.Visibility = Visibility.Collapsed;
        }

        private void TableMail_Click(object sender, RoutedEventArgs e)
        {
            TableMailPostsWindow window = new TableMailPostsWindow();
            window.Title = "Таблица почтовых отделений";
            window.ShowDialog();
        }

        private void TableClient_Click(object sender, RoutedEventArgs e)
        {
            TableClientsWindow window = new TableClientsWindow();
            window.Title = "Таблица клиентов";
            window.ShowDialog();
        }

        private void TableParcel_Click(object sender, RoutedEventArgs e)
        {
            TableParcelsWindow window = new TableParcelsWindow();
            window.Title = "Посылки всех почтовых отделений";
            window.ShowDialog();
        }

        private void TableInventory_Click(object sender, RoutedEventArgs e)
        {
            TableInventoriesWindow window = new TableInventoriesWindow();
            window.Title = "Описи всех посылок";
            window.ShowDialog();
        }

        private void AllParcels_Click(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource = null;
            DataGrid.Items.Refresh();
            MainGrid.Visibility = Visibility.Collapsed;
            TextLabel.Visibility = Visibility.Collapsed;
            UserText.Visibility = Visibility.Collapsed;
            FindExpensive.Visibility = Visibility.Collapsed;
            FindByProdName.Visibility = Visibility.Collapsed;
            FragileOrNot.Visibility = Visibility.Collapsed;
            FindFragile.Visibility = Visibility.Collapsed;
            SolvingGrid.Visibility = Visibility.Visible;
            using (SqlConnection connection = new SqlConnection($"Data Source={MainWindow.SqlServer};Initial Catalog=Ustinov; Integrated Security =True"))
            {
                connection.Open();
                string query = "select MailPost.m_index, MailPost.m_address, " +
                               "(select count(*) from Parcel where Parcel.p_office = MailPost.m_id) as ParcelsCount from MailPost";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                ds.Tables[0].Columns[0].ColumnName = "Индекс";
                ds.Tables[0].Columns[1].ColumnName = "Адрес";
                ds.Tables[0].Columns[2].ColumnName = "Количество посылок";
                DataGrid.ItemsSource = ds.Tables[0].DefaultView;
                DataGrid.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                DataGrid.CanUserAddRows = false;
                DataGrid.CanUserDeleteRows = false;
            }
        }

        private void FindMostExpensive_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Visibility = Visibility.Collapsed;
            FindByProdName.Visibility = Visibility.Collapsed;
            FindFragile.Visibility = Visibility.Collapsed;
            FragileOrNot.Visibility = Visibility.Collapsed;
            SolvingGrid.Visibility = Visibility.Visible;
            TextLabel.Visibility = Visibility.Visible;
            UserText.Visibility = Visibility.Visible;
            FindExpensive.Visibility = Visibility.Visible;
            TextLabel.Content = "Введите индекс почтового отделения";
            UserText.Text = "";
            DataGrid.ItemsSource = null;
            DataGrid.Items.Refresh();
        }

        private void FindExpensive_Click(object sender, RoutedEventArgs e)
        {
            decimal dvalue;
            if (!decimal.TryParse(UserText.Text, out dvalue) || string.IsNullOrWhiteSpace(UserText.Text))
            {
                MessageBox.Show("Индекс должен иметь длину до 9 символов и состоять только из цифр");
                return;
            }
            using (SqlConnection connection = new SqlConnection($"Data Source={MainWindow.SqlServer};Initial Catalog=Ustinov; Integrated Security =True"))
            {
                connection.Open();
                string query = "select max(tb.somemoney) as MaximumTaxAndPrice " +
                    "from(select(Parcel.p_tax + Inventory.i_price) as somemoney from Parcel, Inventory, MailPost " +
                    $"where Parcel.p_inventory = Inventory.i_id and Parcel.p_office = MailPost.m_id and MailPost.m_index = {dvalue}) as tb";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                ds.Tables[0].Columns[0].ColumnName = "Максимальная (цена+налог) для почтового отделения с индексом " + dvalue.ToString();
                DataGrid.ItemsSource = ds.Tables[0].DefaultView;
                DataGrid.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                DataGrid.CanUserAddRows = false;
                DataGrid.CanUserDeleteRows = false;
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            SolvingGrid.Visibility = Visibility.Collapsed;
            MainGrid.Visibility = Visibility.Visible;
        }

        private void ProductNameMailPosts_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Visibility = Visibility.Collapsed;
            FindExpensive.Visibility = Visibility.Collapsed;
            FindFragile.Visibility = Visibility.Collapsed;
            FragileOrNot.Visibility = Visibility.Collapsed;
            SolvingGrid.Visibility = Visibility.Visible;
            TextLabel.Visibility = Visibility.Visible;
            UserText.Visibility = Visibility.Visible;
            FindByProdName.Visibility = Visibility.Visible;
            TextLabel.Content = "Введите название продукта";
            UserText.Text = "";
            DataGrid.ItemsSource = null;
            DataGrid.Items.Refresh();
        }

        private void FindByProdName_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserText.Text))
            {
                MessageBox.Show("Строка ввода не должна быть пустой");
                return;
            }
            using (SqlConnection connection = new SqlConnection($"Data Source={MainWindow.SqlServer};Initial Catalog=Ustinov; Integrated Security =True"))
            {
                connection.Open();
                string query = "select distinct MailPost.m_address " +
                               "from MailPost, Inventory, Parcel " +
                               $"where '{UserText.Text}' in (select Inventory.i_product_name from Inventory where Inventory.i_id = Parcel.p_inventory) and Parcel.p_office = MailPost.m_id";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                ds.Tables[0].Columns[0].ColumnName = "Адреса почтовых отделений, где есть посылка с именем " + UserText.Text;
                DataGrid.ItemsSource = ds.Tables[0].DefaultView;
                DataGrid.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                DataGrid.CanUserAddRows = false;
                DataGrid.CanUserDeleteRows = false;
            }
        }

        private void FragileParcels_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Visibility = Visibility.Collapsed;
            FindExpensive.Visibility = Visibility.Collapsed;
            FindByProdName.Visibility = Visibility.Collapsed;
            UserText.Visibility = Visibility.Collapsed;
            FindFragile.Visibility = Visibility.Visible;
            SolvingGrid.Visibility = Visibility.Visible;
            TextLabel.Visibility = Visibility.Visible;
            FragileOrNot.Visibility = Visibility.Visible;
            var fragile = new List<string>() { "хрупкая", "не хрупкая" };
            FragileOrNot.ItemsSource = fragile;
            TextLabel.Content = "Выберите тип посылки";
            UserText.Text = "";
            DataGrid.ItemsSource = null;
            DataGrid.Items.Refresh();
        }

        private void FindFragile_Click(object sender, RoutedEventArgs e)
        {
            if (FragileOrNot.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите значение");
                return;
            }
            using (SqlConnection connection = new SqlConnection($"Data Source={MainWindow.SqlServer};Initial Catalog=Ustinov; Integrated Security =True"))
            {
                connection.Open();
                string ans = FragileOrNot.Text == "хрупкая" ? "yes" : "no";
                string query = "select MailPost.m_address, " +
                              $"(select count(*) from Parcel, Inventory where Parcel.p_office = MailPost.m_id and Inventory.i_fragile = '{ans}' and Parcel.p_inventory = Inventory.i_id) as FragileCount " +
                               "from MailPost " +
                               "group by MailPost.m_address, MailPost.m_id " +
                              $"having(select count(*) from Parcel, Inventory where Parcel.p_office = MailPost.m_id and Inventory.i_fragile = '{ans}' and Parcel.p_inventory = Inventory.i_id) > 0";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                ds.Tables[0].Columns[0].ColumnName = "Адрес почтового отделения";
                ds.Tables[0].Columns[1].ColumnName = ans == "yes" ? "Количество хрупких посылок":"Количество не хрупких посылок";
                DataGrid.ItemsSource = ds.Tables[0].DefaultView;
                DataGrid.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                DataGrid.CanUserAddRows = false;
                DataGrid.CanUserDeleteRows = false;
            }
        }

        private void NoClientsPosts_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Visibility = Visibility.Collapsed;
            FindExpensive.Visibility = Visibility.Collapsed;
            FindByProdName.Visibility = Visibility.Collapsed;
            UserText.Visibility = Visibility.Collapsed;
            FindFragile.Visibility = Visibility.Collapsed;
            TextLabel.Visibility = Visibility.Collapsed;
            FragileOrNot.Visibility = Visibility.Collapsed;
            SolvingGrid.Visibility = Visibility.Visible;
            using (SqlConnection connection = new SqlConnection($"Data Source={MainWindow.SqlServer};Initial Catalog=Ustinov; Integrated Security =True"))
            {
                connection.Open();
                string query = "select MailPost.m_index, MailPost.m_address from MailPost where MailPost.m_index != all (select Client.c_index from Client)";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                ds.Tables[0].Columns[0].ColumnName = "Индекс";
                ds.Tables[0].Columns[1].ColumnName = "Адрес";
                DataGrid.ItemsSource = ds.Tables[0].DefaultView;
                DataGrid.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                DataGrid.CanUserAddRows = false;
                DataGrid.CanUserDeleteRows = false;
            }
        }

        private void ClientsParcels_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Visibility = Visibility.Collapsed;
            FindExpensive.Visibility = Visibility.Collapsed;
            FindByProdName.Visibility = Visibility.Collapsed;
            UserText.Visibility = Visibility.Collapsed;
            FindFragile.Visibility = Visibility.Collapsed;
            TextLabel.Visibility = Visibility.Collapsed;
            FragileOrNot.Visibility = Visibility.Collapsed;
            SolvingGrid.Visibility = Visibility.Visible;
            using (SqlConnection connection = new SqlConnection($"Data Source={MainWindow.SqlServer};Initial Catalog=Ustinov; Integrated Security =True"))
            {
                connection.Open();
                string query = "select Client.c_FN, MailPost.m_index, MailPost.m_address, " +
                               "(case when Inventory.i_product_name is null then 'нет посылок' else cast(Inventory.i_product_name as nvarchar(50)) end) as Product " +
                               "from Inventory " +
                               "join Parcel on Inventory.i_id = Parcel.p_inventory " +
                               "full join Client on Parcel.p_reciever = Client.c_id " +
                               "full join MailPost on Client.c_index = MailPost.m_index " +
                               "where Client.c_FN is not null";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                ds.Tables[0].Columns[0].ColumnName = "ФИО";
                ds.Tables[0].Columns[1].ColumnName = "Индекс";
                ds.Tables[0].Columns[2].ColumnName = "Адрес";
                ds.Tables[0].Columns[3].ColumnName = "Имя продукта(если есть)";
                DataGrid.ItemsSource = ds.Tables[0].DefaultView;
                DataGrid.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                DataGrid.CanUserAddRows = false;
                DataGrid.CanUserDeleteRows = false;
            }
        }
    }
}

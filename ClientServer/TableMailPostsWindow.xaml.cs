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
    /// Логика взаимодействия для TableMailPostsWindow.xaml
    /// </summary>
    public partial class TableMailPostsWindow : Window
    {
        public TableMailPostsWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            using (SqlConnection connection = new SqlConnection($"Data Source={MainWindow.SqlServer};Initial Catalog=Ustinov; Integrated Security =True"))
            {
                connection.Open();
                string query = $"SELECT * FROM MailPost";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                TableMailPostsGrid.ItemsSource = ds.Tables[0].DefaultView;
                TableMailPostsGrid.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                TableMailPostsGrid.CanUserAddRows = false;
                TableMailPostsGrid.CanUserDeleteRows = false;
            }
            EditMailPost.IsEnabled = false;
            DeleteMailPost.IsEnabled = false;
        }

        private void AddMailPost_Click(object sender, RoutedEventArgs e)
        {
            AddEditMailPostWindow window = new AddEditMailPostWindow();
            window.Title = "Добавление почтового отделения";
            window.Edit.Visibility = Visibility.Collapsed;
            window.Add.Visibility = Visibility.Visible;
            window.ShowDialog();
        }

        private void EditMailPost_Click(object sender, RoutedEventArgs e)
        {
            AddEditMailPostWindow window = new AddEditMailPostWindow();
            DataRowView row = (DataRowView)TableMailPostsGrid.SelectedItems[0];
            window.NewIndex.Text = row[1].ToString();
            window.NewAddress.Text = row[2].ToString();
            window.NewPhone.Text = row[3].ToString();
            window.NewPostamatCount.Text = row[4].ToString();
            window.IdLabel.Content = "Id " + row[0].ToString();
            window.Title = "Изменение информации об имеющемся почтовом отделении";
            window.Add.Visibility = Visibility.Collapsed;
            window.Edit.Visibility = Visibility.Visible;
            window.ShowDialog();
        }

        private void DeleteMailPost_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection($"Data Source={MainWindow.SqlServer};Initial Catalog=Ustinov; Integrated Security =True"))
            {
                connection.Open();
                DataRowView row = (DataRowView)TableMailPostsGrid.SelectedItems[0];
                SqlCommand Delete = new SqlCommand("P_MailPost_Delete", connection);
                Delete.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter Id = new SqlParameter
                {
                    ParameterName = "@id",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = Convert.ToInt32(row[0])
                };
                Delete.Parameters.Add(Id);
                var deletedid = Delete.ExecuteNonQuery();
                string query = $"SELECT * FROM MailPost";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                TableMailPostsGrid.ItemsSource = ds.Tables[0].DefaultView;
            }
            EditMailPost.IsEnabled = false;
            DeleteMailPost.IsEnabled = false;
            TableMailPostsGrid.SelectedItem = null;
        }

        private void UpdateGrid_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection($"Data Source={MainWindow.SqlServer};Initial Catalog=Ustinov; Integrated Security =True"))
            {
                connection.Open();
                string query = $"SELECT * FROM MailPost";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                TableMailPostsGrid.ItemsSource = ds.Tables[0].DefaultView;
            }
            EditMailPost.IsEnabled = false;
            DeleteMailPost.IsEnabled = false;
            TableMailPostsGrid.SelectedItem = null;
        }

        private void TableMailPostsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditMailPost.IsEnabled = true;
            DeleteMailPost.IsEnabled = true;
        }
    }
}

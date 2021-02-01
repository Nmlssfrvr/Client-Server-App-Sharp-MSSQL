using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
    /// Логика взаимодействия для TableParcelsWindow.xaml
    /// </summary>
    public partial class TableParcelsWindow : Window
    {
        public TableParcelsWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;

            using (SqlConnection connection = new SqlConnection($"Data Source={MainWindow.SqlServer};Initial Catalog=Ustinov; Integrated Security =True"))
            {
                connection.Open();
                string query = MainWindow.admin ? $"SELECT * FROM Parcel" : $"SELECT * FROM Parcel WHERE Parcel.p_office = {WorkerWindow.MailId}";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                TableParcelsGrid.ItemsSource = ds.Tables[0].DefaultView;
                TableParcelsGrid.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                TableParcelsGrid.CanUserAddRows = false;
                TableParcelsGrid.CanUserDeleteRows = false;
            }
            EditParcel.IsEnabled = false;
            DeleteParcel.IsEnabled = false;
        }

        private void AddParcel_Click(object sender, RoutedEventArgs e)
        {
            using (UstinovContext db = new UstinovContext())
            {
                var recievers = MainWindow.admin ? db.Clients.Select(c => new { c.CId, c.CFn }).ToList():db.Clients.Where(c => c.CIndex == WorkerWindow.MailIndex).Select(c => new { c.CId, c.CFn }).ToList();
                var inventories = db.Inventories.Select(i => new { i.IId, i.IProductName }).ToList();
                var offices = MainWindow.admin ? db.MailPosts.Select(m => new { m.MId, m.MIndex }).ToList():db.MailPosts.Where(m => m.MId == WorkerWindow.MailId).Select(m => new { m.MId, m.MIndex }).ToList();
                var tariffs = new List<string>() { "normal", "speed", "precious" };
                AddParcelWindow window = new AddParcelWindow();
                window.NewReciever.ItemsSource = recievers.ConvertAll(el => el.CId.ToString() + " " + el.CFn);
                window.NewInventory.ItemsSource = inventories.ConvertAll(el => el.IId.ToString() + " " + el.IProductName);
                window.NewOffice.ItemsSource = offices.ConvertAll(el => el.MId.ToString() + " " + el.MIndex.ToString());
                window.NewTariff.ItemsSource = tariffs;
                window.Title = "Добавление новой посылки";
                window.ShowDialog();
            }
        }

        private void EditParcel_Click(object sender, RoutedEventArgs e)
        {
            using (UstinovContext db = new UstinovContext())
            {
                var recievers = MainWindow.admin ? db.Clients.Select(c => new { c.CId, c.CFn }).ToList() : db.Clients.Where(c => c.CIndex == WorkerWindow.MailIndex).Select(c => new { c.CId, c.CFn }).ToList();
                var inventories = db.Inventories.Select(i => new { i.IId, i.IProductName }).ToList();
                var offices = MainWindow.admin ? db.MailPosts.Select(m => new { m.MId, m.MIndex }).ToList() : db.MailPosts.Where(m => m.MId == WorkerWindow.MailId).Select(m => new { m.MId, m.MIndex }).ToList();
                var tariffs = new List<string>() { "normal", "speed", "precious" };
                EditParcelWindow window = new EditParcelWindow();
                window.NewReciever.ItemsSource = recievers.ConvertAll(el => el.CId.ToString() + " " + el.CFn);
                window.NewInventory.ItemsSource = inventories.ConvertAll(el => el.IId.ToString() + " " + el.IProductName);
                window.NewOffice.ItemsSource = offices.ConvertAll(el => el.MId.ToString() + " " + el.MIndex.ToString());
                window.NewTariff.ItemsSource = tariffs;
                DataRowView row = (DataRowView)TableParcelsGrid.SelectedItems[0];
                window.NewTax.Text = row[1].ToString();
                window.NewReciever.SelectedItem = recievers.ConvertAll(el => el.CId.ToString() + " " + el.CFn).Find(el => el.Split(" ")[0] == row[2].ToString());
                window.NewInventory.SelectedItem = inventories.ConvertAll(el => el.IId.ToString() + " " + el.IProductName).Find(el => el.Split(" ")[0] == row[3].ToString());
                window.NewOffice.SelectedItem = offices.ConvertAll(el => el.MId.ToString() + " " + el.MIndex.ToString()).Find(el => el.Split(" ")[0] == row[4].ToString());
                window.NewTariff.SelectedItem = tariffs.Find(el => el == row[5].ToString());
                window.IdLabel.Content = "Id " + row[0].ToString();
                window.Title = "Изменение информации об имеющейся посылке";
                window.ShowDialog();
            }
        }

        private void TableParcelsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditParcel.IsEnabled = true;
            DeleteParcel.IsEnabled = true;
        }

        private void DeleteParcel_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection($"Data Source={MainWindow.SqlServer};Initial Catalog=Ustinov; Integrated Security =True"))
            {
                connection.Open();
                DataRowView row = (DataRowView)TableParcelsGrid.SelectedItems[0];
                SqlCommand Delete = new SqlCommand("P_Parcel_Delete", connection);
                Delete.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter Id = new SqlParameter
                {
                    ParameterName = "@id",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = Convert.ToInt32(row[0])
                };
                Delete.Parameters.Add(Id);
                var deletedid = Delete.ExecuteNonQuery();
                string query = MainWindow.admin ? $"SELECT * FROM Parcel" : $"SELECT * FROM Parcel WHERE Parcel.p_office = {WorkerWindow.MailId}";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                TableParcelsGrid.ItemsSource = ds.Tables[0].DefaultView;
            }
            EditParcel.IsEnabled = false;
            DeleteParcel.IsEnabled = false;
            TableParcelsGrid.SelectedItem = null;
        }

        private void UpdateGrid_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection($"Data Source={MainWindow.SqlServer};Initial Catalog=Ustinov; Integrated Security =True"))
            {
                connection.Open();
                string query = MainWindow.admin ? $"SELECT * FROM Parcel" : $"SELECT * FROM Parcel WHERE Parcel.p_office = {WorkerWindow.MailId}";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                TableParcelsGrid.ItemsSource = ds.Tables[0].DefaultView;
            }
            EditParcel.IsEnabled = false;
            DeleteParcel.IsEnabled = false;
            TableParcelsGrid.SelectedItem = null;
        }
    }
}

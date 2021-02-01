using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ClientServer
{
    /// <summary>
    /// Логика взаимодействия для ParcelsWindow.xaml
    /// </summary>
    public partial class ParcelsWindow : Window
    {
        public class Parcel
        {
            public string Name { get; set; }
            public int Amount { get; set; }
            public string Address { get; set; }
            public decimal Tax { get; set; }
            public Parcel(string name, int amount, string address, decimal tax)
            {
                this.Name = name;
                this.Amount = amount;
                this.Address = address;
                this.Tax = tax;
            }
        };
        public ParcelsWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }
        public virtual DbSet<Parcel> ParcelSet { get; set; }
        private void ParcelsDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            using (UstinovContext db = new UstinovContext())
            {
                List<Parcel> parcelslist = new List<Parcel>();
                parcelslist = db.ParcelsTable(MainWindow.UserId).ToList();
                ParcelsDataGrid.ItemsSource = parcelslist;
                ParcelsDataGrid.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                ParcelsDataGrid.CanUserAddRows = false;
                ParcelsDataGrid.CanUserDeleteRows = false;
            }
        }
    }
}

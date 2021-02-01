using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для WorkerWindow.xaml
    /// </summary>
    public partial class WorkerWindow : Window
    {
        public static int MailId;
        public static decimal MailIndex;
        public WorkerWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        private void TParcel_Click(object sender, RoutedEventArgs e)
        {
            using (UstinovContext db = new UstinovContext())
            {
                var mail = db.Clients.Where(c => c.CId == MainWindow.UserId).Join(db.MailPosts, c => c.CIndex, m => m.MIndex, (c, m) => new { m.MId, m.MIndex }).FirstOrDefault();
                MailId = mail.MId;
                MailIndex = mail.MIndex;
            }
            TableParcelsWindow window = new TableParcelsWindow();
            window.Title = MainWindow.admin ?  "Посылки всех почтовых отделений":"Все посылки почтового отделения с индексом " + MailIndex.ToString();
            window.ShowDialog();
        }

        private void TInventory_Click(object sender, RoutedEventArgs e)
        {
            TableInventoriesWindow window = new TableInventoriesWindow();
            window.Title = "Описи посылок";
            window.ShowDialog();
        }
    }
}

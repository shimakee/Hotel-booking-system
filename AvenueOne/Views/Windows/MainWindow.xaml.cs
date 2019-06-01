using AvenueOne.Views;
using AvenueOne.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AvenueOne
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AdminPage adminpage = new AdminPage();

        public MainWindow()
        {
            InitializeComponent();
            MainContent.Content = adminpage;
        }

        private void Button_BookingPage(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new BookingPage();
        }

        private void Button_AdminPage(object sender, RoutedEventArgs e)
        {
            MainContent.Content = adminpage;
        }

        private void Button_Logout(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}

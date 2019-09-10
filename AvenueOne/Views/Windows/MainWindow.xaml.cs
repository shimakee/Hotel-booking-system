using AvenueOne.Persistence.Repositories;
using AvenueOne.Utilities;
using AvenueOne.Views;
using AvenueOne.Views.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private PlutoContext _plutoContext = new PlutoContext();

        AdminPage adminPage = new AdminPage();
        BookingPage bookingPage = new BookingPage();
        SettingsPage settingsPage = new SettingsPage();


        public MainWindow()
        {
            InitializeComponent();
            MainContent.Content = adminPage;
        }

        private void Button_BookingPage(object sender, RoutedEventArgs e)
        {
            MainContent.Content = bookingPage;
        }

        private void Button_AdminPage(object sender, RoutedEventArgs e)
        {
            MainContent.Content = adminPage;
        }

        private void Button_SettingsPage(object sender, RoutedEventArgs e)
        {
            MainContent.Content = settingsPage;
        }

        private void Button_Logout(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            _plutoContext.Dispose();
        }
    }
}

using AvenueOne.Interfaces;
using AvenueOne.Models;
using AvenueOne.Properties;
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

namespace AvenueOne.Views.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void SettingsPage_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSettings();
        }

        private void Button_SaveSettings(object sender, RoutedEventArgs e)
        {
            IUser user = new UserModel(Username.Text,Password.Text, IsAdmin.IsChecked ?? false,  Id.Text);
            Settings.Default["UserAccount"] = user;
            Settings.Default.Save();
        }

        private void Button_LoadSettings(object sender, RoutedEventArgs e)
        {
            LoadSettings();
        }

        private void LoadSettings()
         {
            IUser user = (UserModel)Settings.Default["UserAccount"];
            Id.Text = user.Id?.ToString() ?? "";
            Username.Text = user.Username?.ToString() ?? "";
            Password.Text = user.Password?.ToString() ?? "";
            IsAdmin.IsChecked = user.IsAdmin;
        }
    }
}

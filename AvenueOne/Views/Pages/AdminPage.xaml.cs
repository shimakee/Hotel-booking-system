using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Interfaces.ViewModelInterfaces;
using AvenueOne.Properties;
using AvenueOne.Utilities;
using AvenueOne.Utilities.Tools;
using AvenueOne.ViewModels.PagesViewModels;
using AvenueOne.Views.Windows;
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
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        private IRegistrationParentViewModel _adminViewModel;

        public AdminPage()
        {
            InitializeComponent();
            _adminViewModel = new AdminPageViewModel();
            DataContext = _adminViewModel;
        }

        private void AddUser(object sender, RoutedEventArgs e)
        {
            //IUser userAccount = Settings.Default["UserAccount"] as IUser;
            //if (userAccount.IsAdmin)
            //{
                RegistrationWindow registrationwindow = new RegistrationWindow(_adminViewModel);
                registrationwindow.Owner = Window.GetWindow(this);
                //must be show dialog to avoid changing windows
                registrationwindow.ShowDialog();
            //}
            //else
            //{
            //    MessageBox.Show("User is not allowed, only accounts with admin access are able to execute command. ");
            //}
        }
    }
}

using AvenueOne.Interfaces;
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

        public AdminPage()
        {
            InitializeComponent();
            IViewModel adminViewModel = new AdminPageViewModel(SampleData.GetUsersList());
            DataContext = adminViewModel;
        }

        private void AddUser(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationwindow = new RegistrationWindow();
            registrationwindow.Owner = Window.GetWindow(this);
            //must be show dialog to avoid changing windows
            registrationwindow.ShowDialog();
        }
    }
}

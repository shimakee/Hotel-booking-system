using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Interfaces.ViewModelInterfaces;
using AvenueOne.Models;
using AvenueOne.Utilities;
using AvenueOne.Utilities.Tools;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.ModelViewModel;
using AvenueOne.ViewModels.WindowsViewModels;
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
using System.Windows.Shapes;

namespace AvenueOne.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            IUnitOfWork unitOfWork= new UnitOfWork();
            ILoginService loginService = new LoginService(unitOfWork);
            IUserViewModel userViewModel = new UserViewModel(new User());
            ILoginViewModel loginWindowViewModel = new LoginWindowViewModel(this, loginService, userViewModel);
            DataContext = loginWindowViewModel;
        }

        private void Button_Close(object sender, RoutedEventArgs e) //no need to complicate and implement an ICommand?
        {
            this.Close();
        }
    }
}

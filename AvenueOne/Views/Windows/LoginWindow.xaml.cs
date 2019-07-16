using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Models;
using AvenueOne.Utilities;
using AvenueOne.Utilities.Tools;
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
            IUser userModel = new UserModel();
            IUnitOfWork unitOfWork= new UnitOfWork();
            unitOfWork.Users.AddRange(SampleData.GetUsersList());
            ILoginProcessor loginProcessor = new LoginProcessor(unitOfWork);
            IUserValidator userModelValidator = new ValidatorUserModel();
            ILoginViewModel loginWindowViewModel = new LoginWindowViewModel(userModel, loginProcessor, userModelValidator);
            DataContext = loginWindowViewModel;
        }

        private void Button_Close(object sender, RoutedEventArgs e) //no need to complicate and implement an ICommand?
        {
            this.Close();
        }
    }
}

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

namespace AvenueOne.Views.Windows
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
            //IUserModel userModel = new UserModel();
            IPerson personModel = new PersonModel();
            IUserValidator userModelValidator = new ValidatorUserModel();
            //IViewModel registrationWindowViewModel = new RegistrationWindowViewModel(userModel, personModel, userModelValidator);
            IUnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.Users.AddRange(SampleData.GetUsersList());
            IRegistrationViewModel registrationWindowViewModel = new RegistrationWindowViewModel(userModelValidator, unitOfWork);
            DataContext = registrationWindowViewModel;
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void CloseWindow()
        {
            this.Close();
        }
    }
}

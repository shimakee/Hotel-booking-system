using AvenueOne.Interfaces;
using AvenueOne.Models;
using AvenueOne.Utilities;
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
            IPersonModel personModel = new PersonModel();
            IUserModelValidator userModelValidator = new ValidatorUserModel();
            //IViewModel registrationWindowViewModel = new RegistrationWindowViewModel(userModel, personModel, userModelValidator);
            IAddUserProcessor addUserProcessor = new AddUserProcessor();
            IViewModel registrationWindowViewModel = new RegistrationWindowViewModel(userModelValidator, addUserProcessor);
            DataContext = registrationWindowViewModel;
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

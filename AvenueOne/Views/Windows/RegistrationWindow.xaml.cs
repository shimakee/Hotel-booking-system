using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Interfaces.ViewModelInterfaces;
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
        public IRegistrationViewModel _registrationWindowViewModel {get; private set;}

        public RegistrationWindow()
        {
            InitializeComponent();
            IPerson personModel = new PersonModel();
            IUnitOfWork unitOfWork = new UnitOfWork();
            _registrationWindowViewModel = new RegistrationWindowViewModel(unitOfWork);
            DataContext = _registrationWindowViewModel;
        }

        public RegistrationWindow(IRegistrationParentViewModel parentViewModel)
            :this()
        {
            _registrationWindowViewModel.UserAdded += parentViewModel.UserAdded;
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

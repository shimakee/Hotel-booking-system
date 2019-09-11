using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Interfaces.ViewModelInterfaces;
using AvenueOne.Models;
using AvenueOne.Persistence.Repositories;
using AvenueOne.Services;
using AvenueOne.Services.Interfaces;
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

namespace AvenueOne.Views.Windows
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        //private PlutoContext PlutoContext = new PlutoContext();
        private PlutoContext _plutoContext;

        //public IRegistrationViewModel _registrationWindowViewModel {get; private set;}

        public RegistrationWindow(PlutoContext plutoContext)
        {
            InitializeComponent();
            if (plutoContext == null)
                throw new ArgumentNullException("Context cannot be null.");

            this._plutoContext = plutoContext;

            IUnitOfWork unitOfWork = new UnitOfWork(_plutoContext);
            IDisplayService displayService = new WpfDisplayService();
            AddUserCommand addUserCommand = new AddUserCommand(displayService, unitOfWork);

            IUserViewModel userViewModel = new UserViewModel(new User());
            IPersonViewModel personViewModel = new PersonViewModel(new Person());
            IRegistrationViewModel _registrationWindowViewModel = new RegistrationWindowViewModel(this, addUserCommand, userViewModel, personViewModel);

            DataContext = _registrationWindowViewModel;
        }

        //public RegistrationWindow(IRegistrationParentViewModel parentViewModel)
        //    :this()
        //{
        //    _registrationWindowViewModel.UserAdded += parentViewModel.UserAdded;
        //}

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            //_plutoContext.Dispose();
        }
    }
}

using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Models;
using AvenueOne.Persistence.Repositories;
using AvenueOne.Services;
using AvenueOne.Services.Interfaces;
using AvenueOne.Utilities;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.UserCommands;
using AvenueOne.ViewModels.WindowsViewModels;
using System;
using System.Windows;

namespace AvenueOne.Views.Windows
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private PlutoContext _plutoContext;
        public IRegistrationViewModel ViewModel { get; private set; }

        public RegistrationWindow(PlutoContext plutoContext)
        {
            InitializeComponent();

            this._plutoContext = plutoContext ?? throw new ArgumentNullException("Context cannot be null.");

            IUser user = new User();
            user.Person = new Person();
            IDisplayService displayService = new WpfDisplayService();
            IUnitOfWork unitOfWork = new UnitOfWork(_plutoContext);
            AddUserCommand addUserCommand = new AddUserCommand(displayService, unitOfWork);

            IRegistrationViewModel registrationWindowViewModel = new RegistrationWindowViewModel(this, addUserCommand, user);

            this.ViewModel = registrationWindowViewModel;
            DataContext = registrationWindowViewModel;
        }

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

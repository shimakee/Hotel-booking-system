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

namespace AvenueOne.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private PlutoContext _context = new PlutoContext();
        public LoginWindow()
        {
            InitializeComponent();
            IUnitOfWork unitOfWork= new UnitOfWork(_context);
            ILoginService loginService = new LoginService(unitOfWork);
            IUserViewModel userViewModel = new UserViewModel(new User());
            IDisplayService displayService = new WpfDisplayService();
            ILoginViewModel loginWindowViewModel = new LoginWindowViewModel(this,
                                                                                                                                    new LoginCommand(loginService, displayService),
                                                                                                                                    userViewModel);
            DataContext = loginWindowViewModel;
        }

        private void Button_Close(object sender, RoutedEventArgs e) //no need to complicate and implement an ICommand?
        {
            this.Close();
        }

        private void On_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Users.ToList();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            _context.Dispose();
        }
    }
}

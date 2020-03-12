using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Models;
using AvenueOne.Persistence.Repositories;
using AvenueOne.Services;
using AvenueOne.Services.Interfaces;
using AvenueOne.Utilities;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.WindowCommands;
using AvenueOne.ViewModels.WindowsViewModels;
using System;
using System.Windows;

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
            IUser user = new User();
            IDisplayService displayService = new WpfDisplayService();
            BaseWindowCommand closeWindowCommand = new CloseWindowCommand();
            ILoginViewModel loginWindowViewModel = new LoginWindowViewModel(this,
                                                                                                                                    closeWindowCommand,
                                                                                                                                    new LoginCommand(loginService, displayService),
                                                                                                                                    user);
            DataContext = loginWindowViewModel;
        }
        private void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(e.ClickCount >= 2)
            {
                if (this.WindowState == WindowState.Maximized)
                {
                    this.WindowState = WindowState.Normal;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                }
            }
            else
            {
                this.DragMove();

            }
        }
        private void MinimizeWindow(object sender, RoutedEventArgs e) //no need to complicate and implement an ICommand?
        {
            this.WindowState = WindowState.Minimized;
        }
        private void MaximizeWindow(object sender, RoutedEventArgs e) 
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void On_Loaded(object sender, RoutedEventArgs e)
        {
            //commented out eager loading since context will be disposed anyway after login.
            //_context.Users.ToList();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            _context.Dispose();
        }
    }
}

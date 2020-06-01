using AvenueOne.Persistence.Repositories;
using AvenueOne.Utilities;
using AvenueOne.ViewModels.WindowsViewModels;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using AvenueOne.Views;
using AvenueOne.Views.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AvenueOne.Models;
using System.Data.Entity;
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
using AvenueOne.ViewModels.Commands.WindowCommands;

namespace AvenueOne
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PlutoContext _plutoContext;

        public MainWindow()
        {
            InitializeComponent();
            this._plutoContext = new PlutoContext();

            BaseWindowCommand closeWindowCommand = new CloseWindowCommand();
            IMainWindowViewModel mainWindowViewModel = new MainWindowViewModel(this, closeWindowCommand, _plutoContext);
            DataContext = mainWindowViewModel;
        }

        private void Button_Logout(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //eager loading data; - careful with this since it slows app loading time
            //_plutoContext.Users.Load();
            //_plutoContext.People.Load();
            //_plutoContext.Customers.Load();
            _plutoContext.Customers.Include(c => c.Person)
                                                    .ToList();
            _plutoContext.Users.Include(u => u.Person)
                                                    .ToList();
            _plutoContext.RoomType.Include(r => r.Amenities)
                                                    .ToList();
            _plutoContext.Amenities.Include(a => a.RoomTypes)
                                                    .ToList();
            _plutoContext.Room.Include(r => r.RoomType)
                                                    .ToList();
            _plutoContext.Bookings.Include(b=> b.Room)
                                                    .ToList();
            _plutoContext.Transactions.Include(t => t.Customer)
                                                        .Include(t => t.Employee)
                                                        .Include(t => t.Bookings)
                                                        .ToList();
            //_plutoContext.People.Include(p => p.Customer)
            //                                    .Include(p=> p.User)
            //                                    .ToList();

        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            _plutoContext.Dispose();
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            _plutoContext.Dispose();
        }

        private void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 1)
            {
                //    if (this.WindowState == WindowState.Maximized)
                //    {
                //        this.WindowState = WindowState.Normal;
                //    }
                //    else
                //    {
                //        this.WindowState = WindowState.Maximized;
                //    }
                //}
                //else
                //{

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
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

    }
}

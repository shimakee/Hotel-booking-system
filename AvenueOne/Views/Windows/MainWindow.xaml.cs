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

            IMainWindowViewModel mainWindowViewModel = new MainWindowViewModel(this, _plutoContext);
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
            //_plutoContext.People.Include(p => p.Customer)
            //                                    .Include(p=> p.User)
            //                                    .ToList();

        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            _plutoContext.Dispose();
        }
    }
}

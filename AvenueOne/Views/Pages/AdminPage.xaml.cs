﻿using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Models;
using AvenueOne.Persistence.Repositories;
using AvenueOne.Services;
using AvenueOne.Services.Interfaces;
using AvenueOne.Utilities;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.PagesViewModels;
using AvenueOne.ViewModels.TabViewModels;
using AvenueOne.ViewModels.WindowsViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AvenueOne.Views.Pages
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        private PlutoContext _context;
        public AdminPageViewModel ViewModel { get; private set; }
        public AdminPage(PlutoContext context)
        {
            if (context == null)
                throw new ArgumentNullException("Context cannot be null.");

            InitializeComponent();

            this._context = context;
            IUser User = new User();
            User.Person = new Person();
            ICustomer Customer = new Customer();
            Customer.Person = new Person();
            IDisplayService displayService = new WpfDisplayService();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            RegisterUserCommand RegisterUserCommand = new RegisterUserCommand(context);
            EditProfileCommand editProfileCommand = new EditProfileCommand(unitOfWork, 
                                                                                                                                displayService);
            RemoveUserCommand removeUserCommand = new RemoveUserCommand(unitOfWork, 
                                                                                                                                        displayService);
            EditCustomerCommand editCustomerCommand = new EditCustomerCommand(unitOfWork, displayService);
            OpenCustomerWindowCommand openCustomerWindowCommand = new OpenCustomerWindowCommand(context);
            CustomerTabViewModel customerTab = new CustomerTabViewModel(new Person(), Customer, _context.Customers.Local, editCustomerCommand, openCustomerWindowCommand);
            AdminPageViewModel _adminViewModel = new AdminPageViewModel(Window.GetWindow(this), 
                                                                                                                                RegisterUserCommand, 
                                                                                                                                editProfileCommand,
                                                                                                                                removeUserCommand,
                                                                                                                                User,
                                                                                                                                _context.Users.Local,
                                                                                                                                customerTab);


            this.ViewModel = _adminViewModel;
            DataContext = _adminViewModel;
        }

        //used a bit of code behind because its easier to implement
        private void Refresh_UsersList(object sender, RoutedEventArgs e)
        {
            if (_context != null && ViewModel != null)
            {
                _context.Users.ToList();
                this.ViewModel.UsersList = _context.Users.Local;
            }
            UsersList.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var person = this.ViewModel.Person;
            //var user = this.ViewModel.User;
            //MessageBox.Show($"Username:{user.Username}\nFull name:{person.FullName}.");
            if (_context != null)
                _context.SaveChanges();
        }
    }
}

﻿using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Interfaces.ViewModelInterfaces;
using AvenueOne.Models;
using AvenueOne.Persistence.Repositories;
using AvenueOne.Properties;
using AvenueOne.Utilities;
using AvenueOne.Utilities.Tools;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.ModelViewModel;
using AvenueOne.ViewModels.PagesViewModels;
using AvenueOne.ViewModels.WindowsViewModels;
using AvenueOne.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AvenueOne.Views.Pages
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage(PlutoContext context)
        {
            if (context == null)
                throw new ArgumentNullException("Context cannot be null.");

            InitializeComponent();

            IUser User = new User();
            IPerson Person = new Person();
            RegisterUserCommand RegisterUserCommand = new RegisterUserCommand(context);
            AdminPageViewModel _adminViewModel = new AdminPageViewModel(Window.GetWindow(this), RegisterUserCommand, new UserViewModel(User), new PersonViewModel(Person));
            _adminViewModel.UsersList = new ObservableCollection<IUser>(context.Users.ToList());
            DataContext = _adminViewModel;
        }
    }
}

using AvenueOne.Core;
using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Models;
using AvenueOne.Persistence;
using AvenueOne.Persistence.Repositories;
using AvenueOne.Properties;
using AvenueOne.Services;
using AvenueOne.Services.Interfaces;
using AvenueOne.Utilities;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.ClassCommands;
using AvenueOne.ViewModels.Commands.UserCommands;
using AvenueOne.ViewModels.Commands.WindowCommands;
using AvenueOne.ViewModels.PagesViewModels;
using AvenueOne.ViewModels.TabViewModels;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;

namespace AvenueOne.Views.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        private PlutoContext _context;
        public SettingsPageViewModel ViewModel { get; private set; }
        public SettingsPage(PlutoContext context)
        {
            if (context == null)
            {
                throw new NullReferenceException("context cannot be null.");
            }

            InitializeComponent();

            this._context = context;
            User User = Settings.Default.UserAccount;
            //User.Person = new Person();
            IDisplayService displayService = new WpfDisplayService();
            BaseWindowCommand closeWindowCommand = new CloseWindowCommand();
            IGenericUnitOfWork<User> genericUnitOfWorkUser = new GenericUnitOfWork<User>(_context);
            BaseClassCommand<User> createUserCommand = new CreateUserCommand(genericUnitOfWorkUser, displayService);
            BaseClassCommand<User> updateUserCommand = new UpdateUserCommand(genericUnitOfWorkUser, displayService);
            BaseClassCommand<User> deleteUserCommand = new DeleteUserCommand(genericUnitOfWorkUser, displayService);
            ClearClassCommand<User> clearUserCommand = new ClearUserCommand();
            IUserViewModel userTab = new UserViewModel(User.Person, User, _context.Users.Local,
                                                                                                        createUserCommand,
                                                                                                        updateUserCommand,
                                                                                                        deleteUserCommand,
                                                                                                        clearUserCommand);

            SettingsPageViewModel settingsPageViewModel = new SettingsPageViewModel(Window.GetWindow(this),
                                                                                                                                            closeWindowCommand,
                                                                                                                                            userTab);


            this.ViewModel = settingsPageViewModel;
            DataContext = settingsPageViewModel;
        }

        //private void SettingsPage_Loaded(object sender, RoutedEventArgs e)
        //{
        //    this.ViewModel.User = _context.Users.Find((User)Settings.Default.UserAccount);
        //}

        //private void Button_SaveSettings(object sender, RoutedEventArgs e)
        //{
        //    IUser user = new User(Username.Text,Password.Text, IsAdmin.IsChecked ?? false,  Id.Text);
        //    Settings.Default["UserAccount"] = user;
        //    Settings.Default.Save();
        //}

        //private void Button_LoadSettings(object sender, RoutedEventArgs e)
        //{
        //    LoadSettings();
        //}

        //private void LoadSettings()
        // {
        //    IUser user = (User)Settings.Default["UserAccount"];
        //    Id.Text = user.Id?.ToString() ?? "";
        //    Username.Text = user.Username?.ToString() ?? "";
        //    Password.Text = user.Password?.ToString() ?? "";
        //    IsAdmin.IsChecked = user.IsAdmin;
        //}
    }
}

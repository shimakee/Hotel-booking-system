using AvenueOne.Persistence.Repositories;
using System;
using AvenueOne.Core.Models;
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
using AvenueOne.Services.Interfaces;
using AvenueOne.Services;
using AvenueOne.Core;
using AvenueOne.ViewModels.Commands;
using AvenueOne.Persistence;
using AvenueOne.ViewModels.Commands.UserCommands;
using AvenueOne.ViewModels.Commands.ClassCommands;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using AvenueOne.ViewModels.TabViewModels;
using AvenueOne.Models;

namespace AvenueOne.Views.Windows
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private PlutoContext _context;
        public UserWindow(PlutoContext context)
        {
           if(context == null)
                throw new ArgumentNullException("Context cannot be null.");

            InitializeComponent();


            this._context = context;
            User User = new User();
            User.Person = new Person();

            IDisplayService displayService = new WpfDisplayService();

            IGenericUnitOfWork<User> genericUnitOfWorkUser = new GenericUnitOfWork<User>(_context);
            BaseClassCommand<User> createUserCommand = new CreateUserCommand(genericUnitOfWorkUser, displayService);
            BaseClassCommand<User> updateUserCommand = new UpdateUserCommand(genericUnitOfWorkUser, displayService);
            BaseClassCommand<User> deleteUserCommand = new DeleteUserCommand(genericUnitOfWorkUser, displayService);
            ClearClassCommand<User> clearUserCommand = new ClearUserCommand();
            IUserViewModel userViewModel = new UserViewModel(new Person(), User, _context.Users.Local,
                                                                                                        createUserCommand,
                                                                                                        updateUserCommand,
                                                                                                        deleteUserCommand,
                                                                                                        clearUserCommand);

            DataContext = userViewModel;
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

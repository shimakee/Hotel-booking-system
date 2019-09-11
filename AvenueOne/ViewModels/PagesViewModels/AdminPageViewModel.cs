using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Interfaces.ViewModelInterfaces;
using AvenueOne.Models;
using AvenueOne.Persistence.Repositories;
using AvenueOne.Properties;
using AvenueOne.Utilities;
using AvenueOne.Utilities.Tools;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.WindowsViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AvenueOne.ViewModels.PagesViewModels
{
    public class AdminPageViewModel : WindowViewModel, IWindowViewModel
    {
        public IPersonViewModel Person { get;  set; }
        public IUserViewModel User { get; set; }
        public RegisterUserCommand RegisterUserCommand { get; private set; }

        AdminPageViewModel(Window window)
            :base(window)
        {
            //GetUsersFromDbCommand = new GetUsersFromDbCommand(this, new UnitOfWork(new PlutoContext()));
            //_usersList = new UnitOfWork(new PlutoContext()).Users.GetAll();
            //UsersList = new ObservableCollection<IUser>(_usersList);

        }

        public AdminPageViewModel(Window window, RegisterUserCommand registerUserCommand, IUserViewModel userViewModel, IPersonViewModel personViewModel)
            : this(window)
        {
            this.User = userViewModel;
            this.Person = personViewModel;
            registerUserCommand.ViewModel = this;
            this.RegisterUserCommand = registerUserCommand;
        }


    }
}

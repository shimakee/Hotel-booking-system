using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Models;
using AvenueOne.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;
using AvenueOne.Properties;
using System.ComponentModel;
using AvenueOne.ViewModels.ModelViewModel;
using System.Windows.Controls;
using AvenueOne.Interfaces.ViewModelInterfaces;

namespace AvenueOne.ViewModels.WindowsViewModels
{
    public class RegistrationWindowViewModel : WindowViewModel, IRegistrationViewModel
    {
        public ICommand AddUserCommand { get; private set; }
        public IUserViewModel User { get; private set; }
        public IPersonViewModel Person { get; private set; }

        RegistrationWindowViewModel(Window window)
            :base (window)
        {
            //this.AddUserCommand = new AddUserCommand(this);
        }

        public RegistrationWindowViewModel(Window registrationWindow, AddUserCommand addUserCommand, IUserViewModel userViewModel, IPersonViewModel personViewModel)
            : this(registrationWindow)
        {
            //this._unitOfWork = unitOfWork;
            this.User = userViewModel;
            this.Person = personViewModel;
            this.AddUserCommand = addUserCommand;
            addUserCommand.ViewModel = this;
            addUserCommand.User = User;
            addUserCommand.Person = Person;
        }
    }
}

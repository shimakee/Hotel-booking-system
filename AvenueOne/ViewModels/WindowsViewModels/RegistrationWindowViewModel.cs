using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Models;
using AvenueOne.ViewModels.Commands.UserCommands;
using System.Windows;
using System;
using AvenueOne.ViewModels.Commands.WindowCommands;

namespace AvenueOne.ViewModels.WindowsViewModels
{
    public class RegistrationWindowViewModel : WindowViewModel, IRegistrationViewModel
    {
        public AddUserCommand AddUserCommand { get; private set; }
        public IUser User { get; set; }

        RegistrationWindowViewModel(Window window, BaseWindowCommand closeWindowCommand)
            :base (window, closeWindowCommand)
        {
        }

        public RegistrationWindowViewModel(Window registrationWindow, BaseWindowCommand closeWindowCommand, AddUserCommand addUserCommand, IUser user)
            : this(registrationWindow, closeWindowCommand)
        {
            //this._unitOfWork = unitOfWork;
            if (user.Person == null)
                throw new ArgumentNullException("Person object inside user of type IUser cannot be null.");

            this.User = user;
            this.AddUserCommand = addUserCommand;
            addUserCommand.ViewModel = this;
            //addUserCommand.User = User;
        }
    }

    
}

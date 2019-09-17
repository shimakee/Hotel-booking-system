using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Models;
using AvenueOne.ViewModels.Commands;
using System.Windows;
using System;

namespace AvenueOne.ViewModels.WindowsViewModels
{
    public class RegistrationWindowViewModel : WindowViewModel, IRegistrationViewModel
    {
        public AddUserCommand AddUserCommand { get; private set; }
        public IUser User { get; set; }

        RegistrationWindowViewModel(Window window)
            :base (window)
        {
        }

        public RegistrationWindowViewModel(Window registrationWindow, AddUserCommand addUserCommand, IUser user)
            : this(registrationWindow)
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

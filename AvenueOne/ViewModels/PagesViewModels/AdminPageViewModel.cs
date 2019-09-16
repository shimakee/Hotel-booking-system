using AvenueOne.Core.Models;
using AvenueOne.Interfaces;
using AvenueOne.Models;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.WindowsViewModels;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace AvenueOne.ViewModels.PagesViewModels
{
    public class AdminPageViewModel : WindowViewModel, IProfileEditViewModel
    {
        public ObservableCollection<User> UsersList { get; set; }
        public RegisterUserCommand RegisterUserCommand { get; private set; }
        public EditProfileCommand EditProfileCommand { get; set; }
        public IUser User { get; set; }

        AdminPageViewModel(Window window)
            :base(window)
        {
        }

        public AdminPageViewModel(Window window, RegisterUserCommand registerUserCommand, EditProfileCommand editProfileCommand, IUser user, ObservableCollection<User> usersList)
            : this(window)
        {

            if (user.Person == null)
                throw new ArgumentNullException("Person object inside user of type IUser cannot be null.");

            this.User = user;
            this.UsersList = usersList;
            this.RegisterUserCommand = registerUserCommand;
            this.RegisterUserCommand.ViewModel = this;
            this.EditProfileCommand = editProfileCommand;
            this.EditProfileCommand.ViewModel = this;

        }

        public void OnUserAdded(object source, UserEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("Event args must not be null.");
            }
            else
            {
                UsersList.Add(e.User);
            }

        }
    }
}

using AvenueOne.Core.Models;
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
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
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
    public class AdminPageViewModel : WindowViewModel, IRegistrationParent, INotifyPropertyChanged
    {
        public IPersonViewModel Person { get;  set; }
        public IUserViewModel User { get; set; }

        public RegisterUserCommand RegisterUserCommand { get; private set; }

        AdminPageViewModel(Window window)
            :base(window)
        {
            UsersList = new ObservableCollection<IUser>();

        }

        public AdminPageViewModel(Window window, RegisterUserCommand registerUserCommand, IUserViewModel userViewModel, IPersonViewModel personViewModel)
            : this(window)
        {
            this.User = userViewModel;
            this.Person = personViewModel;
            registerUserCommand.ViewModel = this;
            this.RegisterUserCommand = registerUserCommand;
        }

        private ObservableCollection<IUser> _usersList;
        public ObservableCollection<IUser> UsersList
        {
            get { return _usersList; }
            set { _usersList = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnUserAdded(object source, UserEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException("Event args must not be null.");

            UsersList.Add(e.User);
        }


    }
}

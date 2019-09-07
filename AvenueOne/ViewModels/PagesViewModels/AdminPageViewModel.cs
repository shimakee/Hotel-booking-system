using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Interfaces.ViewModelInterfaces;
using AvenueOne.Properties;
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
    public class AdminPageViewModel : WindowViewModel
    {
        public ObservableCollection<IUser> UsersList { get; private set; }
        public IPersonViewModel Person { get;  set; }
        public IUserViewModel User { get; set; }
        public ICommand RegisterUserCommand { get; private set; }

        AdminPageViewModel(Window window)
            :base(window)
        {
            RegisterUserCommand = new RegisterUserCommand(this);
            UsersList = new ObservableCollection<IUser>(SampleData.SingeInstance.Users);
        }

        public AdminPageViewModel(Window window, IUserViewModel userViewModel, IPersonViewModel personViewModel)
            : this(window)
        {
            User = userViewModel;
            Person = personViewModel;
            //RegistrationViewModel = registrationViewModel;
        }
    }
}

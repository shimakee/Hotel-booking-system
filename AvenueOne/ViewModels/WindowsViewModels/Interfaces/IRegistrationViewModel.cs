using AvenueOne.Interfaces.ViewModelInterfaces;
using AvenueOne.Models;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.ModelViewModel;
using AvenueOne.ViewModels.WindowsViewModels;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AvenueOne.Interfaces
{
    public interface IRegistrationViewModel : IWindowViewModel
    {
        AddUserCommand AddUserCommand { get; }
        IUserViewModel User { get; }
        IPersonViewModel Person { get;  }

        //event EventHandler<UserEventArgs> UserAdded;
    }
}

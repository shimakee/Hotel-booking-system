using AvenueOne.Interfaces.ViewModelInterfaces;
using AvenueOne.ViewModels.ModelViewModel;
using AvenueOne.ViewModels.WindowsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AvenueOne.Interfaces
{
    public interface IRegistrationViewModel : IWindowViewModel
    {
        //IUserModel AddUser(IUserModel userModel);
        void AddUser(Window sourceWindow, string password, string passwordConfirm);
        IUserViewModel User { get; }
        IPersonViewModel Person { get;  }
        event EventHandler<UserEventArgs> UserAdded;
    }
}

using AvenueOne.Interfaces;
using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.WindowsViewModels.Interfaces
{
    public interface IAdminPageViewModel : INotifyPropertyChanged, IWindowViewModel
    {
        IBaseObservableViewModel<User> UserTab { get; set; }
        IBaseObservableViewModel<Customer> CustomerTab { get; set; }
        IRoomTabViewModel RoomTab { get; set; }
    }
}

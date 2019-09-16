using AvenueOne.Interfaces;
using AvenueOne.Interfaces.ViewModelInterfaces;
using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.WindowsViewModels.Interfaces
{
    public interface IProfileEditViewModel : IWindowViewModel
    {
        IPersonViewModel Person { get; set; }
        IUserViewModel User { get; set; }
        User Account { get; set; }
    }
}

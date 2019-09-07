using AvenueOne.ViewModels.WindowsViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Interfaces.ViewModelInterfaces
{
    public interface IRegistrationParentViewModel
    {
        void UserAdded(object sender, UserEventArgs e);
    }
}

using AvenueOne.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.WindowsViewModels.Interfaces
{
    public interface IAccountViewModel
    {
        IUser UserAccount { get; } // so you can reference the logged in account
    }
}

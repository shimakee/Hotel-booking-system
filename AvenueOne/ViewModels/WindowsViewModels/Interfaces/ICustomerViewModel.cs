using AvenueOne.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.WindowsViewModels.Interfaces
{
    public interface ICustomerViewModel :IAccountViewModel
    {
        IPerson CustomerProfile { get; set; }
        ICustomer Customer { get; set; }
    }
}

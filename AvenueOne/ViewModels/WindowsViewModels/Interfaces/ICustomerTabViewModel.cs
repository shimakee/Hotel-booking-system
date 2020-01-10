using AvenueOne.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.WindowsViewModels.Interfaces
{
    public interface ICustomerTabViewModel
    {
        ObservableCollection<ICustomer> CustomerList { get; set; }
        IPerson CustomerProfile { get; set; }
        ICustomer Customer { get; set; }
    }
}

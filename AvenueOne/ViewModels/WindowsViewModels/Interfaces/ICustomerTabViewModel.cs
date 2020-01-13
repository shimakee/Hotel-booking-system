using AvenueOne.Interfaces;
using AvenueOne.Models;
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
        ObservableCollection<Customer> CustomerList { get; set; }
        IPerson CustomerProfile { get; set; }
        ICustomer Customer { get; set; }
    }
}

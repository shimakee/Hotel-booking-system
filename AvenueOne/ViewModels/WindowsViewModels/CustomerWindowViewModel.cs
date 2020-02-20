using AvenueOne.Interfaces;
using AvenueOne.ViewModels.Commands.WindowCommands;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AvenueOne.ViewModels.WindowsViewModels
{
    public class CustomerWindowViewModel : WindowViewModel, IWindowViewModel
    {
        public CustomerWindowViewModel(Window window, BaseWindowCommand closeWindowCommand, ICustomerViewModel customerViewModel)
            : base(window, closeWindowCommand)
        {
            this.CustomerTab = customerViewModel;
        }

        public ICustomerViewModel CustomerTab { get; set; }
    }
}

using AvenueOne.ViewModels.Commands.WindowCommands;
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
    public interface IWindowViewModel : IAccountViewModel
    {
        Window Window { get; }
        ICommand CloseWindowCommand { get; set; }
    }
}

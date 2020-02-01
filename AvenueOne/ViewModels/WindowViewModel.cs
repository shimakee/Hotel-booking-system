using AvenueOne.Interfaces;
using AvenueOne.Models;
using AvenueOne.Properties;
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

    public abstract class WindowViewModel : AccountViewModel, IWindowViewModel
    {
        public Window Window { get; }
        public BaseWindowCommand CloseWindowCommand { get; set; }

        //public WindowViewModel(Window window, 
        //                                        ShowWindowCommand showWindowCommand, 
        //                                        ShowDialogWindowCommand showDialogWindowCommand,
        //                                        CloseWindowCommand closeWindowCommand)
        public WindowViewModel(Window window, BaseWindowCommand closeWindowCommand)
            :base()
        {
            this.Window = window;
            //this.ShowWindowCommand = showWindowCommand;
            //this.ShowDialogWindowCommand = showDialogWindowCommand;
            this.CloseWindowCommand = closeWindowCommand;
            //this.ShowWindowCommand.ViewModel = this;
            //this.ShowDialogWindowCommand.ViewModel = this;
            this.CloseWindowCommand.ViewModel = this;
            this.CloseWindowCommand.Window = window;
        }
    }
}

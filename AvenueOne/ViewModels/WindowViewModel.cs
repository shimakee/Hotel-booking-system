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

namespace AvenueOne.ViewModels.WindowsViewModels
{

    public abstract class WindowViewModel : AccountViewModel, IWindowViewModel
    {
        public Window Window { get; }
        public ShowWindowCommand ShowWindowCommand { get; set; }
        public ShowDialogWindowCommand ShowDialogWindowCommand { get; set; }
        public CloseWindowCommand CloseWindowCommand { get; set; }

        public WindowViewModel(Window window, 
                                                ShowWindowCommand showWindowCommand, 
                                                ShowDialogWindowCommand showDialogWindowCommand,
                                                CloseWindowCommand closeWindowCommand)
            :base()
        {
            this.Window = window;
            this.ShowWindowCommand = showWindowCommand;
            this.ShowDialogWindowCommand = showDialogWindowCommand;
            this.CloseWindowCommand = closeWindowCommand;
            this.ShowWindowCommand.ViewModel = this;
            this.ShowDialogWindowCommand.ViewModel = this;
            this.CloseWindowCommand.ViewModel = this;
        }
    }
}

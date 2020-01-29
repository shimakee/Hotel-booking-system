using AvenueOne.Services.Interfaces;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AvenueOne.ViewModels.Commands.WindowCommands
{
    public abstract class BaseWindowCommand : ICommand
    {
        public IAccountViewModel ViewModel { get; set; }
        public Window Window { get; set; }
        public BaseWindowCommand()
        {
        }

        public virtual event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter)
        {
            if (ViewModel != null)
                return ViewModel.UserAccount.IsAdmin;
            return false;
        }

        public abstract void Execute(object parameter);
    }
}

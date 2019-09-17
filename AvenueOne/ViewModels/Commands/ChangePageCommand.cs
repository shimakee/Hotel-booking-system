using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AvenueOne.ViewModels.Commands
{
    public class ChangePageCommand : ICommand
    {
        private IMainWindowViewModel ViewModel;

        public ChangePageCommand(IMainWindowViewModel viewModel)
        {
            this.ViewModel = viewModel ?? throw new ArgumentNullException("ViewModel cannot be null.");
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException("A parameter with page name must be passed.");

            try
            {
                string name = parameter as String;
                ViewModel.CurrentPage = ViewModel.Pages[name];
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

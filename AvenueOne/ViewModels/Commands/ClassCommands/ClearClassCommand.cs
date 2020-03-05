using AvenueOne.Core;
using AvenueOne.Interfaces;
using AvenueOne.Models;
using AvenueOne.Services.Interfaces;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvenueOne.ViewModels.Commands.ClassCommands
{
    public class ClearClassCommand<T> : ICommand where T : class, IBaseObservableModel<T>, new()
    {
        public IBaseObservableViewModel<T> ViewModel;
        public ClearClassCommand()
        {
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public virtual void Execute(object parameter)
        {
            try
            {
                Clear();
                Validate();
            }
            catch (Exception exception)
            {
                //TODO: create logger
                throw;
            }
        }
        protected virtual void Validate()
        {
                if (this.ViewModel == null)
                    throw new NullReferenceException("Viewmodel cannot be null.");
                //if (this.ViewModel.Model == null || this.ViewModel.ModelSelected == null)
                //    throw new NullReferenceException("Model or Selection cannot be null.");

        }

        protected virtual void Clear()
        {
            ViewModel.ModelSelected = new T();
        }
    }
}

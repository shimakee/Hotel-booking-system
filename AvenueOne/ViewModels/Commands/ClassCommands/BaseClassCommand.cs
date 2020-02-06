using AvenueOne.Core;
using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvenueOne.ViewModels.Commands
{
    public abstract class BaseClassCommand<T> : IBaseClassCommand<T> where T : class, IBaseObservableModel<T>, new()
    {
        public virtual IBaseObservableViewModel<T> ViewModel { get; set; }
        protected IGenericUnitOfWork<T> _genericUnitOfWork;
        protected IDisplayService _displayService;

        public BaseClassCommand(IGenericUnitOfWork<T> genericUnitOfWork, IDisplayService displayService)
        {
            this._genericUnitOfWork = genericUnitOfWork;
            this._displayService = displayService;
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

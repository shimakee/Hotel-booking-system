using AvenueOne.Core;
using AvenueOne.Interfaces;
using AvenueOne.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvenueOne.ViewModels.Commands
{
    public interface IBaseClassCommand<T> : ICommand where T : class, IBaseObservableModel<T>, new()
    {
        IBaseObservableViewModel<T> ViewModel { get; set; }
        //IGenericUnitOfWork<T> _genericUnitOfWork { get; }
        //IDisplayService _displayService {get; }
    }
}

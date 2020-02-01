using AvenueOne.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels
{
    public interface IObservableWindowViewModel<T> : IWindowViewModel, IBaseObservableViewModel<T> where T : class, IBaseObservableModel<T>, new()
    {
    }
}

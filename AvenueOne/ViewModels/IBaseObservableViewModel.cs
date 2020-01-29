using AvenueOne.Interfaces;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels
{
    public interface IBaseObservableViewModel<T> : IAccountViewModel, INotifyPropertyChanged where T : class, IBaseObservableModel<T>, new()
    {
        ObservableCollection<T> ModelList { get; set; }
        IBaseObservableModel<T> Model { get; set; }
        IBaseObservableModel<T> ModelSelected { get; set; }
    }
}

using AvenueOne.Interfaces;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.ClassCommands;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AvenueOne.ViewModels
{
    public interface IBaseObservableViewModel<T> : IAccountViewModel, INotifyPropertyChanged where T : class, IBaseObservableModel<T>, new()
    {
        Window Window { get; set; }
        BaseClassCommand<T> CreateClassCommand { get; set; }
        BaseClassCommand<T> UpdateClassCommand { get; set; }
        BaseClassCommand<T> DeleteClassCommand { get; set; }
        ClearClassCommand<T> ClearClassCommand { get; set; }
        ObservableCollection<T> ModelList { get; set; }
        T Model { get; set; }
        T ModelSelected { get; set; }
    }
}

using AvenueOne.Interfaces;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.ClassCommands;
using AvenueOne.ViewModels.Commands.WindowCommands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AvenueOne.ViewModels
{
    public class ObservableWindowViewModel<T> : BaseObservableViewModel<T>, IObservableWindowViewModel<T> where T : class, IBaseObservableModel<T>, new()
    {
        public Window Window { get; }
        public ICommand CloseWindowCommand { get; set; }

        public ObservableWindowViewModel(Window window, 
                                                                    T model,
                                                                    ObservableCollection<T> modelList,
                                                                    BaseWindowCommand closeWindowCommand,
                                                                    BaseClassCommand<T> createClassCommand,
                                                                    BaseClassCommand<T> updateClassCommand,
                                                                    BaseClassCommand<T> deleteClassCommand,
                                                                    ClearClassCommand<T> clearClassCommand)
            :base(model, modelList, createClassCommand, updateClassCommand, deleteClassCommand, clearClassCommand)
        {
            this.Window = window;
            closeWindowCommand.ViewModel = this;
            closeWindowCommand.Window = window;
            this.CloseWindowCommand = closeWindowCommand;
            //this.CloseWindowCommand.ViewModel = this;
            //this.CloseWindowCommand.Window = window;
        }
    }
}

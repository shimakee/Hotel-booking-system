using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AvenueOne.Interfaces;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.ClassCommands;
using AvenueOne.ViewModels.WindowsViewModels;

namespace AvenueOne.ViewModels
{
    public class BaseObservableViewModel<T> : AccountViewModel, IBaseObservableViewModel<T> where T : class, IBaseObservableModel<T>, new()
    {
        #region Properties

        public virtual BaseClassCommand<T> CreateClassCommand { get; set; }
        public virtual BaseClassCommand<T> UpdateClassCommand { get; set; }
        public virtual BaseClassCommand<T> DeleteClassCommand { get; set; }
        public virtual ClearClassCommand<T> ClearClassCommand { get; set; }
        private ObservableCollection<T> _modelList;
        public ObservableCollection<T> ModelList
        {
            get { return _modelList; }
            set { _modelList = value;
                OnPropertyChanged();
            }
        }
        private T _model { get; set; }
        public T Model
        {
            get { return _model; }
            set { _model = value;
                OnPropertyChanged();
            }
        }

        private T _modelSelected;
        public virtual T ModelSelected
        {
            get { return _modelSelected; }
            set {
                Model = value;
                if(value != null)
                    _modelSelected = value.Copy();
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructors

        public BaseObservableViewModel(T model,
                                                            ObservableCollection<T> modelList,
                                                            BaseClassCommand<T> createClassCommand,
                                                            BaseClassCommand<T> updateClassCommand,
                                                            BaseClassCommand<T> deleteClassCommand,
                                                            ClearClassCommand<T> clearClassCommand)
            :base()
        {
            this.ModelSelected = model;
            this.ModelList = modelList;
            this.CreateClassCommand = createClassCommand;
            this.UpdateClassCommand = updateClassCommand;
            this.DeleteClassCommand = deleteClassCommand;
            this.ClearClassCommand = clearClassCommand;
            this.CreateClassCommand.ViewModel = this;
            this.UpdateClassCommand.ViewModel = this;
            this.DeleteClassCommand.ViewModel = this;
            this.ClearClassCommand.ViewModel = this;
        }
        #endregion

        #region Utilities

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AvenueOne.Interfaces;
using AvenueOne.ViewModels.Commands.ClassCommands;
using AvenueOne.ViewModels.WindowsViewModels;

namespace AvenueOne.ViewModels
{
    public class BaseObservableViewModel<T> : AccountViewModel, IBaseObservableViewModel<T> where T : class, IBaseObservableModel<T>, new()
    {
        #region Properties
        public CreateClassCommand<T> CreateClassCommand { get; set; }
        public ObservableCollection<T> ModelList { get; set; }
        private IBaseObservableModel<T> _model { get; set; }
        public IBaseObservableModel<T> Model
        {
            get { return _model; }
            set { _model = value;
                OnPropertyChanged();
            }
        }

        private IBaseObservableModel<T> _modelSelected;
        public IBaseObservableModel<T> ModelSelected
        {
            get { return _modelSelected; }
            set {
                Model = value;
                if(value != null)
                    _modelSelected = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructors

        public BaseObservableViewModel(ObservableCollection<T> modelList, CreateClassCommand<T> createClassCommand)
            :base()
        {
            this.ModelList = modelList;
            this.CreateClassCommand = createClassCommand;
            this.CreateClassCommand.ViewModel = this;
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

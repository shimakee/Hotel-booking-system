using AvenueOne.Interfaces;
using AvenueOne.Models;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.WindowCommands;
using AvenueOne.ViewModels.WindowsViewModels;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AvenueOne.ViewModels.PagesViewModels
{
    public class SettingsPageViewModel : WindowViewModel, INotifyPropertyChanged
    {
        #region Ctor
        SettingsPageViewModel(Window window, BaseWindowCommand closeWindowCommand)
            : base(window, closeWindowCommand)
        {
        }

        public SettingsPageViewModel(Window window,
                                                        BaseWindowCommand closeWindowCommand,
                                                        IBaseObservableViewModel<User> userTab)
            : this(window, closeWindowCommand)
        {
            this.UserTab = userTab;
        }
        #endregion

        #region Properties

        public IBaseObservableViewModel<User> UserTab { get; set; }

        private bool _isPasswordIncluded;
        public bool IsPasswordIncluded
        {
            get { return _isPasswordIncluded; }
            set
            {
                _isPasswordIncluded = value;
                OnPropertyChanged();
            }
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}

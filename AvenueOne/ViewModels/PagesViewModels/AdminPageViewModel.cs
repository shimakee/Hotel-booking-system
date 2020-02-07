using AvenueOne.Core.Models;
using AvenueOne.Interfaces;
using AvenueOne.Models;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.WindowCommands;
using AvenueOne.ViewModels.TabViewModels;
using AvenueOne.ViewModels.WindowsViewModels;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AvenueOne.ViewModels.PagesViewModels
{
    public class AdminPageViewModel : WindowViewModel, IAdminPageViewModel
    {

        #region Properties

        public IBaseObservableViewModel<User> UserTab { get; set; }
        public IBaseObservableViewModel<Customer> CustomerTab { get; set; }
        public IRoomTabViewModel RoomTab { get; set; }

            private bool _isPasswordIncluded;
            public bool IsPasswordIncluded
            {
                get { return _isPasswordIncluded; }
                set { _isPasswordIncluded = value;
                    OnPropertyChanged();
                }
            }
        #endregion


        #region Constructor
            public AdminPageViewModel(Window window,
                                                            BaseWindowCommand closeWindowCommand,
                                                            IBaseObservableViewModel<User> userTab,
                                                            IBaseObservableViewModel<Customer> customerTab,
                                                            IRoomTabViewModel roomTab)
                : base(window, closeWindowCommand)
            {
                this.UserTab = userTab;
                this.CustomerTab = customerTab;
                this.RoomTab = roomTab;
            }
        #endregion



        #region Utilities


            public event PropertyChangedEventHandler PropertyChanged;
            public void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        #endregion
    }
}

using AvenueOne.Interfaces;
using AvenueOne.Models;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.ClassCommands;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.TabViewModels
{
    public class UserViewModel : BaseObservableViewModel<User>, IUserViewModel
    {
        public UserViewModel(IPerson person, User user, ObservableCollection<User> userList,
            BaseClassCommand<User> createClassCommand, BaseClassCommand<User> updateClassCommand, BaseClassCommand<User> deleteClassCommand,
            ClearClassCommand<User> clearClassCommand)
            :base(user, userList, createClassCommand, updateClassCommand, deleteClassCommand, clearClassCommand)
        {
            this.Profile = person;
        }

        private User _modelSelected;
        public override User ModelSelected
        {
            get { return _modelSelected; }
            set {
                Model = value;
                if(value != null)
                {
                    Model.PasswordConfirm = value.Password;
                    value.PasswordConfirm = value.Password;

                    _modelSelected = value.Copy();
                    if (value.Person != null)
                        Profile = value.Person.Copy();
                    OnPropertyChanged();
                }

            }
        }

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

        public IPerson Profile
        {
            get { return _modelSelected.Person; }
            set { _modelSelected.Person = value as Person;
                value.User = _modelSelected; // correct reference for when inserting
                OnPropertyChanged();
            }
        }

    }
}

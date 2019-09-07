using AvenueOne.Interfaces;
using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using AvenueOne.Interfaces.ViewModelInterfaces;

namespace AvenueOne.ViewModels.ModelViewModel
{
    public class UserViewModel : ModelViewModel, IUserViewModel
    {
        private IUser _user;
        private string _passwordConfirm;

        public UserViewModel(IUser user)
        {
            _user = user;
        }

        public IUser User
        {
            get { return _user; }
            set
            {
                _user = value;

                OnPropertyChanged();
                OnPropertyChanged("Username");
                OnPropertyChanged("Id");
                OnPropertyChanged("IsAdmin");

            }
        }
        
        //public string PersonId
        //{
        //    get { return _user.PersonId; }
        //    set
        //    {
        //        _user.PersonId = value;
        //        OnPropertyChanged();
        //    }
        //}

        [Required]
        public string Id
        {
            get { return _user.Id; }
            set { _user.Id = value;
                OnPropertyChanged();
            }
        }

        [Required(ErrorMessage = "required.")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "min 5, max 30 chars.")]
        [RegularExpression(@"^([A-z0-9])*$", ErrorMessage ="invalid format.")]
        public string Username
        {
            get { return _user.Username; }
            set {
                //ValidateProperty(value, "Username");
                _user.Username = value;
                OnPropertyChanged();
            }
        }
        
        [Required(ErrorMessage = "required.")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "min 5, max 30 chars.")]
        [RegularExpression(@"^([A-z0-9!@#$%^&*,.?])*$", ErrorMessage = "invalid format.")]
        public string Password
        {
            get { return _user.Password; }
            set { _user.Password = value;
                OnPropertyChanged();
                OnPropertyChanged("PasswordConfirm");
            }
        }
        
        [Compare("Password", ErrorMessage ="must match with password")]
        public string PasswordConfirm
        {
            get
            {
                return _passwordConfirm;
            }
            set
            {
                _passwordConfirm = value;
                OnPropertyChanged();
                OnPropertyChanged("Password");
            }
        }
        
        public bool IsAdmin
        {
            get { return _user.IsAdmin; }
            set { _user.IsAdmin = value;
                OnPropertyChanged();
            }
        }
    }
}

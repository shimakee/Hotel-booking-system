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
        public IUser User { get; private set; }
        private string _passwordConfirm;

        public UserViewModel(IUser user)
        {
            User = user;
        }
        
        [Required]
        public string Id
        {
            get { return User.Id; }
            set { User.Id = value;
                OnPropertyChanged();
            }
        }

        [Required(ErrorMessage = "required.")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "min 5, max 30 chars.")]
        [RegularExpression(@"^([A-z0-9])*$", ErrorMessage ="invalid format.")]
        public string Username
        {
            get { return User.Username; }
            set {
                //ValidateProperty(value, "Username");
                User.Username = value;
                OnPropertyChanged();
            }
        }
        
        [Required(ErrorMessage = "required.")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "min 5, max 30 chars.")]
        [RegularExpression(@"^([A-z0-9!@#$%^&*,.?])*$", ErrorMessage = "invalid format.")]
        public string Password
        {
            get { return User.Password; }
            set { User.Password = value;
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
            get { return User.IsAdmin; }
            set { User.IsAdmin = value;
                OnPropertyChanged();
            }
        }

        //#region PropertyChanged

        //public event PropertyChangedEventHandler PropertyChanged;

        //private void OnPropertyChanged([CallerMemberName] String property = "")
        //{
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        //}
        //#endregion

        //#region IDataErrorInfo
        //string IDataErrorInfo.Error
        //{
        //    get
        //    {
        //        return null;
        //    }
        //}

        //string IDataErrorInfo.this[string property]
        //{
        //    get
        //    {
        //        return ValidateProperty(property);
        //    }
        //}
        //#endregion

        //#region Validation
        //public bool IsValid
        //{
        //    get
        //    {
        //        foreach(KeyValuePair<string, string> error in ErrorCollection)
        //        {
        //            if (error.Value != null)
        //                return false;
        //        }
        //        return true;
        //    }
        //}

        //private string ValidateProperty(string property)
        //{
        //    var context = new ValidationContext(this, null, null) { MemberName = property };
        //    var result = new List<ValidationResult>();
        //    var value = this.GetType().GetProperty(property).GetValue(this);

        //    bool isValid = Validator.TryValidateProperty(value, context, result);

        //    if (!isValid)
        //        return result.First().ErrorMessage;
        //    return null;
        //}
        //#endregion
    }
}

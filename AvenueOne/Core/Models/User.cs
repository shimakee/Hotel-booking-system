using AvenueOne.Converters;
using AvenueOne.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Models
{
    //[TypeConverter(typeof(UserConverter))] // for settings
    [SettingsSerializeAs(SettingsSerializeAs.Xml)] //for settings
    public class User : BaseObservableModel<User>, IUser
    {

        #region Properties
        private string _username;
        private string _password;
        private string _passwordConfirm;
        private bool _isAdmin;

        [Required(ErrorMessage = "required.")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "min 5, max 30 chars.")]
        [RegularExpression(@"^([A-z0-9])*$", ErrorMessage = "invalid format.")]
        public string Username
        {
            get { return _username; }
            set { _username = value;
                OnPropertyChanged();
            }
        }

        [Required(ErrorMessage = "required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "min 5, max 30 chars.")]
        //[RegularExpression(@"^([A-z0-9!@#$%^&*,.?])*$", ErrorMessage = "invalid format.")]
        public string Password
        {
            get { return _password; }
            set { _password = value;
                OnPropertyChanged();
                OnPropertyChanged("PasswordConfirm");
            }
        }

        [Compare(nameof(Password), ErrorMessage = "must match with password")]
        public string PasswordConfirm
        {
            get { return _passwordConfirm; }
            set { _passwordConfirm = value;
                OnPropertyChanged();
                OnPropertyChanged("Password");
            }
        }
        public bool IsAdmin
        {
            get { return _isAdmin; }
            set { _isAdmin = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Reference
        private Person _person;
        public virtual Person Person
        {
            get { return _person; }
            set { _person = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructor
        public User()
            :base()
        {
            this.IsAdmin = false;
        }

        public User(string username)
            :this()
        {
            this.Username = username;
        }

        public User(string username, bool isAdmin)
            :this(username)
        {
            this.IsAdmin = isAdmin;
        }

        public User(bool isAdmin, string id)
            : this()
        {
            this.IsAdmin = isAdmin;
            this.Id = id;
        }

        public User(string username, string password)
            :this(username)
        {
            this.Password = password;
        }

        public User(string username, string password, bool isAdmin)
            :this(username, password)
        {
            this.IsAdmin = isAdmin;
        }

        public User(string username, string password, string id)
            :this(username, password)
        {
            this.Id = id;
        }

        public User(string username, bool isAdmin, string id)
            :this(username,isAdmin)
        {
            this.Id = id;
        }

        public User(string username, string password, bool isAdmin, string id)
            :this(username, password, isAdmin)
        {
            this.Id =id;
        }
        #endregion

        #region MethodsAndOverrides
        public User Clone()
        {
            User user = this.MemberwiseClone() as User;
            return user;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is User))
                return false;

            User user = (User)obj;

            return this.Username == user.Username;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        #endregion
    }
}


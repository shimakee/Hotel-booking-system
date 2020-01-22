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
    public class User : BaseObservableModel, IUser
    {
        private string _id;
        private string _username;
        private string _password;
        private string _passwordConfirm;
        private bool _isAdmin;
        private Person _person;

        #region Properties
        [Required]
        public string Id
        {
            get { return _id; }
            set { _id = value;
                OnPropertyChanged();
            }
        }

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

        [Compare("Password", ErrorMessage = "must match with password")]
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

        //reference
        public virtual Person Person
        {
            get { return _person; }
            set { _person = value;
                OnPropertyChanged();
            }
        }
        #endregion

        //For Xml
        //<User><Id></Id><Username></Username><Password></Password><IsAdmin></IsAdmin><Person></Person></User>

        #region Constructor
        public User()
        {
            this.IsAdmin = false;
            this.Id = GenerateId();
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
        //public static void CopyPropertiesTo<T, TU>(this T source, TU dest)
        //{
        //    var sourceProps = typeof(T).GetProperties().Where(x => x.CanWrite && x.CanRead).ToList();
        //    var destProps = typeof(TU).GetProperties()
        //            .Where(x => x.CanWrite && x.CanRead)
        //            .ToList();

        //    foreach (var sourceProp in sourceProps)
        //    {
        //        if (destProps.Any(x => x.Name == sourceProp.Name))
        //        {
        //            var p = destProps.First(x => x.Name == sourceProp.Name);
        //            if (p.CanWrite)
        //            { // check if the property can be set or no.
        //                p.SetValue(dest, sourceProp.GetValue(source, null), null);
        //            }
        //        }

        //    }

        //}

        public IUser CopyPropertyValues()
        {
            return CopyPropertyValues(new User());
        }
        public IUser CopyPropertyValues(IUser user)
        {
            List<PropertyInfo> propertyList = typeof(IUser).GetProperties().Where(u => u.CanWrite && u.CanRead).ToList();

            foreach( PropertyInfo info in propertyList)
            {
                if(typeof(IUser).GetProperty(info.Name) != null)
                    info.SetValue(user, info.GetValue(this));
            }
            return user;
        }

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

            return this.Username == ((User)obj).Username;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        #endregion
    }
}


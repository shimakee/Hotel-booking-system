using AvenueOne.Converters;
using AvenueOne.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
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
        public string Id
        {
            get { return _id; }
            set { _id = value;
                OnPropertyChanged();
            }
        }
        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value;
                OnPropertyChanged();
            }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value;
                OnPropertyChanged();
            }
        }
        private bool _isAdmin;
        public bool IsAdmin
        {
            get { return _isAdmin; }
            set { _isAdmin = value;
                OnPropertyChanged();
            }
        }

        //reference
        private Person _person;
        public virtual Person Person
        {
            get { return _person; }
            set { _person = value;
                OnPropertyChanged();
            }
        }

        //For Xml
        //<User><Id></Id><Username></Username><Password></Password><IsAdmin></IsAdmin><Person></Person></User>
        //<User><Id></Id><Username></Username><Password></Password><IsAdmin></IsAdmin><Person><Id></Id><FirstName></FirstName><MiddleName></MiddleName><LastName></LastName><MaidenName></MaidenName><Suffix></Suffix><Gender></Gender><CivilStatus></CivilStatus><Nationality></Nationality><BirthDate></BirthDate></Person></User>

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
            return this.Username.GetHashCode();
        }
    }
}


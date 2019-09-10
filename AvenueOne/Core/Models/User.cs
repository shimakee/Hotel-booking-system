using AvenueOne.Converters;
using AvenueOne.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Models
{
    //[TypeConverter(typeof(UserConverter))] // for settings
    [SettingsSerializeAs(SettingsSerializeAs.Xml)] //for settings
    public class User : IUser
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        //reference
        public virtual Person Person { get; set; }

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
        
        private string GenerateId()
        {
            return GenerateId(32);
        }

        private string GenerateId(int length)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException("Id length cannot be less than 1 in length.");

            decimal d = length / 32;
            int repeat = (int)Math.Floor(d);
            StringBuilder Id = new StringBuilder();

            if (repeat > 0)
                for (int i = 0; i < repeat; i++)
                {
                    Id.Append(Guid.NewGuid().ToString("N"));
                }

            return Id.ToString(0, length);
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


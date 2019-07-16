using AvenueOne.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Models
{
    public class UserModel : IUser
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public UserModel()
        {
            this.IsAdmin = false;
            this.Id = GenerateId();
        }

        public UserModel(string username)
            :this()
        {
            this.Username = username;
        }

        public UserModel(string username, string password)
            :this(username)
        {
            this.Password = password;
        }

        public UserModel(string username, string password, bool isAdmin)
            :this(username, password)
        {
            this.IsAdmin = isAdmin;
        }

        public UserModel(string username, string password, string Id)
            :this(username, password)
        {
            this.Id = GenerateId();
        }

        public UserModel(string username, string password, bool isAdmin, string Id)
            :this(username, password, isAdmin)
        {
            this.Id = GenerateId();
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

            if (!(obj is UserModel))
                return false;

            return this.Username == ((UserModel)obj).Username;
        }

        public override int GetHashCode()
        {
            return this.Username.GetHashCode();
        }
    }
}


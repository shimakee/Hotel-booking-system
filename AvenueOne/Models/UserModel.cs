using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Models
{
    public class UserModel //need to interface this
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public UserModel()
        {
            this.IsAdmin = false;
            this.Id = Guid.NewGuid().ToString("N");
        }

        public UserModel(string username, string password)
            :this()
        {
            this.Username = username;
            this.Password = password;
        }

        public UserModel(string username, string password, bool isAdmin)
            :this(username, password)
        {
            this.IsAdmin = isAdmin;
        }
    }
}

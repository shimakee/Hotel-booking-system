using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Core.Models
{
    public class UserEventArgs : EventArgs
    {
        public UserEventArgs(User user)
        {
            this.User = user;
        }
        public User User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Interfaces
{
    public interface IUserModel
    {
        string Id { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        bool IsAdmin { get; set; }
    }
}

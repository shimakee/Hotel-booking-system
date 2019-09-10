using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Interfaces
{
    public interface ILoginService
    {
        User Login(string username, string password);
        User Login(IUser user);
        bool IsValidLogin(string username, string password);
    }
}

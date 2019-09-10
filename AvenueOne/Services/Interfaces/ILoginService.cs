using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Interfaces
{
    public interface ILoginService
    {
        IUser Login(string username, string password);
        IUser Login(IUser user);
        bool IsValidLogin(string username, string password);
    }
}

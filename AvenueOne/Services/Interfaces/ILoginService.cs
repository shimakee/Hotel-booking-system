using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Interfaces
{
    public interface ILoginService
    {
        bool Login(string username, string password);
        bool Login(IUser user);
        IUser GetValidatedDetails(string username, string password);
        bool IsValidLogin(string username, string password);
    }
}

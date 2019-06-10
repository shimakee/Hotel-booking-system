using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Interfaces
{
    public interface ILoginProcessor
    {
        IUserModel GetValidatedDetails(string username, string password);
        bool IsValidLogin(string username, string password);
        bool DoesUsernameExist(string username);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Interfaces
{
    public interface IUserModelValidator
    {
        bool ValidateUserModel(IUserModel userModel);
        bool HasId(string Id);
        bool ValidateId(string Id);
        bool ValidateUserPass(string username, string password);
        bool ValidateUsername(string username);
        bool ValidatePassword(string password);
    }
}

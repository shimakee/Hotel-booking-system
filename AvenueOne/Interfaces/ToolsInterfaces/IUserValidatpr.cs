using AvenueOne.Interfaces.ToolsInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Interfaces
{
    public interface IUserValidator : IModelValidator<IUser>
    {
        //bool ValidateUserModel(IUserModel userModel);
        bool HasId(string Id);
        bool ValidateId(string Id);
        //HasUserPass
        bool ValidateUserPass(string username, string password);
        //HasUsername
        bool ValidateUsername(string username);
        //HasPassword
        bool ValidatePassword(string password);
    }
}

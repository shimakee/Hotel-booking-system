using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AvenueOne.Interfaces
{
    public interface IRegistration
    {
        //IUserModel AddUser(IUserModel userModel);
        void AddUser(Window sourceWindow, IUserModel userModel, string passwordConfirm);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Interfaces.ViewModelInterfaces
{
    public interface IUserViewModel : IUser
    {

        IUser User { get; set; }
        string PasswordConfirm { get; set; }
    }
}

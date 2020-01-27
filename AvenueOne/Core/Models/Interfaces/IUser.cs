using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Interfaces
{
    public interface IUser : IBaseObservableModel<User>
    {
        string Username { get; set; }
        string Password { get; set; }
        string PasswordConfirm { get; set; }
        bool IsAdmin { get; set; }
        Person Person { get; set; }
    }
}

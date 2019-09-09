using AvenueOne.Interfaces.ViewModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AvenueOne.Interfaces
{
    public interface ILoginViewModel : IWindowViewModel
    {
        IUserViewModel User { get; }
        //void Login(string username, string password);
    }
}

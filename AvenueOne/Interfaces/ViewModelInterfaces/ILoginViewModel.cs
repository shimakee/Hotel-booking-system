using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AvenueOne.Interfaces
{
    public interface ILoginViewModel: IWindowViewModel
    {
        void Login(Window sourceWindow, string username, string password);
    }
}

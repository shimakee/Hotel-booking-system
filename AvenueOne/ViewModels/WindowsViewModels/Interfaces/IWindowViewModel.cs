using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AvenueOne.Interfaces
{
    public interface IWindowViewModel
    {
        IUser UserAccount { get; } // so you can reference the logged in account
        Window Window { get; }
    }
}

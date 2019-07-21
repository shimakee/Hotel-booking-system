using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AvenueOne.Interfaces
{
    public interface IViewModel
    {
        IUser UserAccount { get; }
        void Close(Window sourceWindow); // do i really need to implement this?
    }
}

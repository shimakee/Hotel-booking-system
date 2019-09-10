using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Services.Interfaces
{
    public interface IDisplayService: IWindowGenerator
    {
        void DisplayMessage(string message);
    }
}

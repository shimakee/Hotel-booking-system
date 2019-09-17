using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Services.Interfaces
{
    public interface IDisplayService: IWindowGenerator
    {
        void MessageDisplay(string message);
        void MessageDisplay(string message, string caption);
        void ErrorDisplay(string message, string caption);
        bool MessagePrompt(string message, string caption);
        bool? MessagePromptNullable(string message, string caption);

    }
}

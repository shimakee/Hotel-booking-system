using AvenueOne.Interfaces;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AvenueOne.ViewModels.Commands.WindowCommands
{
    public class CloseWindowCommand : BaseWindowCommand
    {
        public CloseWindowCommand()
            :base()
        {
        }

        public override void Execute(object parameter)
        {
            //if (Window == null && parameter == null && ViewModel.Window == null)
            if (Window == null && parameter == null)
                throw new ArgumentNullException("Both paramter and window assignment cannot be null.\nPlease assign one of these.\n-A window on command property named 'window' upon initialization.\n-A window as parameter arguemnt on xaml front end.");
            //if (Window == null || ViewModel.Window == null && parameter != null)
            if (Window == null && parameter != null)
            {
                object[] values = (object[])parameter;
                this.Window = values[0] as Window;
            }
            //if (Window == null && ViewModel.Window != null)
                //this.Window = ViewModel.Window;

            Window.Close();
        }
    }
}

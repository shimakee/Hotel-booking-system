using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AvenueOne.ViewModels.Commands.WindowCommands
{
    public class ShowDialogWindowCommand : BaseWindowCommand
    {
        public ShowDialogWindowCommand()
            :base()
        {
        }
        public override void Execute(object parameter)
        {
            if (Window == null && parameter == null)
                throw new ArgumentNullException("Both paramter and window assignment cannot be null.\nPlease assign one of these.\n-A window on command property named 'window' upon initialization.\n-A window as parameter arguemnt on xaml front end.");
            if (Window == null && parameter != null)
            {
                object[] values = (object[])parameter;
                this.Window = values[0] as Window;
            }

            Window.ShowDialog();
        }
    }
}

using AvenueOne.Interfaces;
using AvenueOne.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AvenueOne.ViewModels.WindowsViewModels
{
    public abstract class WindowViewModel : IWindowViewModel
    {
        public IUser UserAccount { get; private set; }
        public Window Window { get; }

        public WindowViewModel(Window window)
        {
            this.UserAccount = Settings.Default["UserAccount"] as IUser;
            this.Window = window;
        }
    }
}

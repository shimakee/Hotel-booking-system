using AvenueOne.Interfaces;
using AvenueOne.Properties;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.WindowsViewModels
{
    public abstract class AccountViewModel : IAccountViewModel
    {
        public IUser UserAccount { get; private set; }

        public AccountViewModel()
        {
            this.UserAccount = Settings.Default.UserAccount as IUser;
        }
    }
}

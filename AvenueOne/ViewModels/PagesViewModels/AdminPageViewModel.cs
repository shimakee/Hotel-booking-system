using AvenueOne.Interfaces;
using AvenueOne.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AvenueOne.ViewModels.PagesViewModels
{
    public class AdminPageViewModel : IViewModel
    {
        public IUser UserAccount { get; private set; }
        public IEnumerable<IUser> UsersList { get; private set; }

        public AdminPageViewModel()
        {
            UserAccount = Settings.Default["UserAccount"] as IUser;
        }

        public AdminPageViewModel(IEnumerable<IUser> users)
            : this()
        {
            UsersList = users;
        }

        public void Close(Window sourceWindow)
        {
            sourceWindow.Close();
        }
    }
}

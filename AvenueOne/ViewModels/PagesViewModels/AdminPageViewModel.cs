using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Interfaces.ViewModelInterfaces;
using AvenueOne.Properties;
using AvenueOne.Utilities.Tools;
using AvenueOne.ViewModels.WindowsViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AvenueOne.ViewModels.PagesViewModels
{
    public class AdminPageViewModel : IRegistrationParentViewModel
    {
        public IUser UserAccount { get; private set; }
        public ObservableCollection<IUser> UsersList { get; private set; }
        
        public AdminPageViewModel()
        {
            UserAccount = Settings.Default["UserAccount"] as IUser;
            UsersList = new ObservableCollection<IUser>(SampleData.SingeInstance.Users);
        }

        public void Close(Window sourceWindow)
        {
            sourceWindow.Close();
        }

        public void UserAdded(object sender, UserEventArgs e)
        {
            UsersList.Add(e.User);
        }
    }
}

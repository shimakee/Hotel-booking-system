using AvenueOne.Interfaces;
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
        public IEnumerable<IUser> UsersList { get; private set; }

        public AdminPageViewModel(IEnumerable<IUser> users)
        {
            UsersList = users;
        }

        public bool AccountIsAdmin => throw new NotImplementedException();

        public void Close(Window sourceWindow)
        {
            throw new NotImplementedException();
        }
    }
}

using AvenueOne.Interfaces;
using AvenueOne.Interfaces.ViewModelInterfaces;
using AvenueOne.Models;
using AvenueOne.Properties;
using AvenueOne.Utilities;
using AvenueOne.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AvenueOne.ViewModels.WindowsViewModels
{
    public class LoginWindowViewModel: WindowViewModel, ILoginViewModel
    {
        public ICommand LoginCommand { get; private set; }
        public IUserViewModel User { get;  private set; }

        #region Ctor
        LoginWindowViewModel(Window window)
            :base(window)
        {
            //LoginCommand = new LoginCommand(this); //how to decouple? - also pass as depndency injection?
        }

        //public LoginWindowViewModel(Window loginWindow, ILoginService loginService, IUserViewModel userViewModel)
        public LoginWindowViewModel(Window loginWindow, LoginCommand loginCommand, IUserViewModel userViewModel)
            : this(loginWindow)
        {
            //this._loginService = loginService;
            this.User = userViewModel;
            this.LoginCommand = loginCommand;
            loginCommand.ViewModel = this;
            loginCommand.User = User;
        }
        #endregion
    }
}

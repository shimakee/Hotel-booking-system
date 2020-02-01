using AvenueOne.Interfaces;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.WindowCommands;
using System.Windows;
using System.Windows.Input;

namespace AvenueOne.ViewModels.WindowsViewModels
{
    public class LoginWindowViewModel: WindowViewModel, ILoginViewModel
    {
        public ICommand LoginCommand { get; private set; }
        public IUser User { get; set; }

        #region Ctor
        LoginWindowViewModel(Window window, BaseWindowCommand closeWindowCommand)
            :base(window, closeWindowCommand)
        {
        }

        public LoginWindowViewModel(Window loginWindow, BaseWindowCommand closeWindowCommand, LoginCommand loginCommand, IUser user)
            : this(loginWindow, closeWindowCommand)
        {
            this.User = user;
            this.LoginCommand = loginCommand;
            loginCommand.ViewModel = this;
        }
        #endregion
    }
}

using AvenueOne.Interfaces;
using AvenueOne.ViewModels.Commands;
using System.Windows;
using System.Windows.Input;

namespace AvenueOne.ViewModels.WindowsViewModels
{
    public class LoginWindowViewModel: WindowViewModel, ILoginViewModel
    {
        public ICommand LoginCommand { get; private set; }
        public IUser User { get; set; }

        #region Ctor
        LoginWindowViewModel(Window window)
            :base(window)
        {
        }

        public LoginWindowViewModel(Window loginWindow, LoginCommand loginCommand, IUser user)
            : this(loginWindow)
        {
            this.User = user;
            this.LoginCommand = loginCommand;
            loginCommand.ViewModel = this;
        }
        #endregion
    }
}

using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.Commands.UserCommands;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;

namespace AvenueOne.Interfaces
{
    public interface IRegistrationViewModel : IUserCrudViewModel
    {
        AddUserCommand AddUserCommand { get; }

        //event EventHandler<UserEventArgs> UserAdded;
    }
}

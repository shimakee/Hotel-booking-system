using AvenueOne.ViewModels.Commands;

namespace AvenueOne.Interfaces
{
    public interface IRegistrationViewModel : IWindowViewModel
    {
        AddUserCommand AddUserCommand { get; }
        IUser User { get; }

        //event EventHandler<UserEventArgs> UserAdded;
    }
}

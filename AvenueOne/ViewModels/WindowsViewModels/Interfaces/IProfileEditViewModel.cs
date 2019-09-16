using AvenueOne.Interfaces;

namespace AvenueOne.ViewModels.WindowsViewModels.Interfaces
{
    public interface IProfileEditViewModel : IWindowViewModel
    {
        IUser User { get; set; }
    }
}

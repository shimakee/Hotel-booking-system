using AvenueOne.Interfaces;

namespace AvenueOne.ViewModels.WindowsViewModels.Interfaces
{
    public interface IProfileEditViewModel : IUserCrudViewModel
    {
        IUser Account { get; set; }
        IPerson Profile { get; set; }
        bool IsPasswordIncluded { get; set; }
    }
}

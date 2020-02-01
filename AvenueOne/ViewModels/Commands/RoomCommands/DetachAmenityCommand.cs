using AvenueOne.Core.Models;
using AvenueOne.Core.Models.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Services.Interfaces;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvenueOne.ViewModels.Commands.RoomCommands
{
    public class DetachAmenityCommand : ICommand
    {
        public IRoomTypeViewModel ViewModel;
        private IUnitOfWork _unitOfWork;
        private IDisplayService _displayService;
        public DetachAmenityCommand(IUnitOfWork unitOfWork, IDisplayService displayService)
            //:base(unitOfWork, displayService)
        {
            this._unitOfWork = unitOfWork;
            this._displayService = displayService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (ViewModel != null)
                return ViewModel.UserAccount.IsAdmin;
            return false;
        }

        public async void Execute(object parameter)
        {
            try
            {
                if (ViewModel == null)
                    throw new ArgumentNullException("ViewModel cannot be null.");
                if (ViewModel.AmenitiesSelected == null || ViewModel.ModelSelected == null)
                    throw new ArgumentNullException("Amenities list or selection cannot be null.");
                if (ViewModel.ModelSelected == null)
                    throw new ArgumentNullException("Room type cannot be null.");

                IRoomType roomType = await Task.Run(()=>_unitOfWork.RoomType.GetAsync(ViewModel.ModelSelected.Id));
                roomType.Amenities.Remove(ViewModel.AmenitiesSelected as Amenities);
                int n = await Task.Run(() => _unitOfWork.CompleteAsync());
                if (n <= 0)
                    throw new InvalidOperationException("Could not detach amenity from room type.");
                _displayService.MessageDisplay($"Detached {ViewModel.AmenitiesSelected.Name} from {ViewModel.ModelSelected.Id}.\nAffected rows {n}");

            }
            catch(ArgumentNullException argEx)
            {
                _displayService.ErrorDisplay(argEx.Message, "Null reference error.");
            }
            catch(InvalidOperationException invalidOpEx)
            {
                _displayService.ErrorDisplay(invalidOpEx.Message, "Invalid operation error.");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

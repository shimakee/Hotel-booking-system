using AvenueOne.Core.Models.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Services.Interfaces;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AvenueOne.Core.Models;

namespace AvenueOne.ViewModels.Commands.RoomCommands
{
    public class LinkAmenityToRoomTypeCommand : ICommand
    {
        public IAmenitiesListWindowViewModel ViewModel { get; set; }
        private IUnitOfWork _unitOfWork;
        private IDisplayService _displayService;
        public LinkAmenityToRoomTypeCommand(IUnitOfWork unitOfWork, IDisplayService displayService)
            //: base(unitOfWork, displayService)
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

        public async  void Execute(object parameter)
        {
            try
            {
                if (ViewModel == null)
                    throw new ArgumentNullException("ViewModel cannot be null.");
                if (ViewModel.AmenitiesSelected == null || ViewModel.RoomType == null)
                    throw new ArgumentNullException("Amenities selected and room type cannot be null.");

                IRoomType roomType = await Task.Run(() => _unitOfWork.RoomType.GetAsync(ViewModel.RoomType.Id)) ?? throw new InvalidOperationException("room type does not exist.");
                if (roomType.Amenities.Contains(ViewModel.AmenitiesSelected))
                    throw new InvalidOperationException($"Amenity {ViewModel.AmenitiesSelected.Name} is already link to room type {roomType.Name}.");

                    roomType.Amenities.Add(ViewModel.AmenitiesSelected as Amenities);
                int n = await Task.Run(() => _unitOfWork.CompleteAsync());
                if (n <= 0)
                    throw new InvalidOperationException("Could not add amenities to room type.");
                _displayService.MessageDisplay($"Added amenity {ViewModel.AmenitiesSelected.Name} to room type {ViewModel.RoomType.Name}");
                ViewModel.Window.Close();
            }
            catch (ArgumentNullException argEx)
            {
                _displayService.ErrorDisplay(argEx.Message, "Argument null reference error.");
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

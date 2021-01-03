using AvenueOne.Core;
using AvenueOne.Core.Models;
using AvenueOne.Core.Models.Interfaces;
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
    public class LinkAmenityCommand : ICommand
    {
        public IRoomTypeViewModel ViewModel { get; set; }
        private IGenericUnitOfWork<RoomType> _genericUnitOfWork;
        private IDisplayService _displayService;


        public LinkAmenityCommand(IGenericUnitOfWork<RoomType> genericUnitOfWork, IDisplayService displayService)
        {
            this._genericUnitOfWork = genericUnitOfWork;
            this._displayService = displayService;
        }

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public async void Execute(object parameter)
        {
            try
            {
                if (ViewModel == null)
                    throw new ArgumentNullException("ViewModel cannot be null.");
                if (ViewModel.AmenitiesSelected == null)
                    throw new ArgumentNullException("An amenity must be selected and cannot be null.");
                if (ViewModel.ModelSelected == null)
                    throw new ArgumentNullException("A room type must be selected and cannot be null.");

                RoomType roomType = await Task.Run(() => _genericUnitOfWork.Repositories[typeof(RoomType)].GetAsync(ViewModel.Model.Id)) ?? throw new InvalidOperationException("Room type does not exist.");
                if (roomType.Amenities.Contains(ViewModel.AmenitiesSelected))
                    throw new InvalidOperationException("Amenities already exist in this room type.");

                roomType.Amenities.Add(ViewModel.AmenitiesSelected as Amenities);
                int n = await Task.Run(() => _genericUnitOfWork.CompleteAsync());
                if (n <= 0)
                    throw new InvalidOperationException("Could not detach amenity from room type.");
                _displayService.MessageDisplay($"Detached {ViewModel.AmenitiesSelected.Id} from {ViewModel.ModelSelected.Id}.\nAffected rows {n}");

                ViewModel.ModelSelected = new RoomType();
                ViewModel.ModelSelected = roomType;
            }
            catch (ArgumentNullException argEx)
            {
                _displayService.ErrorDisplay(argEx.Message, "Null reference error.");
            }
            catch (InvalidOperationException invalidOpEx)
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

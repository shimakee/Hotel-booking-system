using AvenueOne.Core.Models;
using AvenueOne.Core.Models.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Services.Interfaces;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvenueOne.ViewModels.Commands.RoomCommands
{
    public class EditAmenitiesCommand : BaseClassCommand, ICommand
    {
        public IAmenitiesViewModel ViewModel;

        public EditAmenitiesCommand(IUnitOfWork unitOfWork, IDisplayService displayService)
            :base(unitOfWork, displayService)
        {
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
                    throw new NullReferenceException("Viewmodel cannot be null.");
                if (ViewModel.Amenities == null || ViewModel.AmenitiesSelected == null)
                    throw new NullReferenceException("Ameneties cannot be null.");

                if (!ViewModel.AmenitiesSelected.IsValid || !ViewModel.Amenities.IsValid)
                    throw new ValidationException("Invalid input on amenities.");

                IAmenities amenities = await Task.Run(()=>_unitOfWork.Amenities.GetAsync(ViewModel.Amenities.Id)) ?? throw new InvalidOperationException("Amenity does not exist.");
                ViewModel.AmenitiesSelected.DeepCopyTo(amenities as Amenities);
                int n = await Task.Run(() => _unitOfWork.CompleteAsync());

                if (n == 0)
                    throw new InvalidOperationException("Could not edit amenities.");
                _displayService.MessageDisplay($"Changed amenities.\nName to:{ViewModel.AmenitiesSelected.Name}\nAffected rows:{n}", "Amenities edited");
            }
            catch (ValidationException validationException)
            {
                _displayService.ErrorDisplay(validationException.Message, "Validation error!");
            }
            catch (InvalidOperationException ex)
            {
                _displayService.ErrorDisplay(ex.Message, "Error on amenities");
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}

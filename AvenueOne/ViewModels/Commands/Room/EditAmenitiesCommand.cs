using AvenueOne.Core.Models;
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

namespace AvenueOne.ViewModels.Commands.Room
{
    public class EditAmenitiesCommand : ICommand
    {
        public IAmenitiesViewModel ViewModel;
        private IUnitOfWork _unitOfWork;
        private IDisplayService _displayService;

        public EditAmenitiesCommand(IUnitOfWork unitOfWork, IDisplayService displayService)
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
                    throw new NullReferenceException("Viewmodel cannot be null.");
                if (ViewModel.Amenities == null || ViewModel.AmenitiesSelected == null)
                    throw new NullReferenceException("Ameneties cannot be null.");

                if (!ViewModel.AmenitiesSelected.IsValid || !ViewModel.Amenities.IsValid)
                    throw new ValidationException("Invalid input on amenities.");

                Amenities amenities = _unitOfWork.Amenities.Get(ViewModel.Amenities.Id) ?? throw new InvalidOperationException("Could not delete, amenity does not exist.");
                ViewModel.AmenitiesSelected.CopyPropertyValuesTo(amenities);
                int n = await Task.Run(() => _unitOfWork.CompleteAsync());

                if (n == 0)
                    throw new InvalidOperationException("Could not edit amenities.");

                _displayService.MessageDisplay($"Changed amenities.\nName from:{ViewModel.Amenities.Name}\nName to:{ViewModel.AmenitiesSelected.Name}\nAffected rows:{n}", "Amenities edited");
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

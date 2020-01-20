using AvenueOne.Core.Models;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Services.Interfaces;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvenueOne.ViewModels.Commands.Room
{
    public class RemoveAmenitiesCommand : ICommand
    {
        public IAmenitiesViewModel ViewModel;
        private IUnitOfWork _unitOfWork;
        private IDisplayService _displayService;

        public RemoveAmenitiesCommand(IUnitOfWork unitOfWork, IDisplayService displayService)
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

                if (!ViewModel.Amenities.IsValid)
                    throw new ValidationException("Invalid input on amenities.");

                Amenities amenities = _unitOfWork.Amenities.Get(ViewModel.Amenities.Id) ?? throw new InvalidOperationException("Could not delete item, amenity does not exist.");

                _unitOfWork.Amenities.Remove(amenities);

                int n = await Task.Run(() => _unitOfWork.CompleteAsync());

                if (n == 0)
                    throw new InvalidOperationException("Could not remove amenities.");

                _displayService.MessageDisplay($"Removed amenities.\nName:{ViewModel.AmenitiesSelected.Name}\nAffected rows:{n}", "Amenities added");
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

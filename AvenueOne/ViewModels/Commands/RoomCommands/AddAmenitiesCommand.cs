using AvenueOne.Core.Models;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Persistence.Repositories;
using AvenueOne.Services.Interfaces;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvenueOne.ViewModels.Commands.RoomCommands
{
    public class AddAmenitiesCommand : ICommand
    {
        public IAmenitiesWindowViewModel ViewModel;
        private IUnitOfWork _unitOfWork;
        private IDisplayService _displayService;

        public AddAmenitiesCommand(IUnitOfWork unitOfWork, IDisplayService displayService)
        {
            this._unitOfWork = unitOfWork;
            this._displayService = displayService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (this.ViewModel != null)
                return this.ViewModel.UserAccount.IsAdmin;
            return false;
        }

        public async void Execute(object parameter)
        {
            try
            {
                if (ViewModel == null)
                    throw new NullReferenceException("Viewmodel cannot be null.");
                if (ViewModel.Amenities == null)
                    throw new NullReferenceException("Ameneties cannot be null.");

                if(!ViewModel.Amenities.IsValid)
                    throw new ValidationException("Invalid input on amenities.");
                Amenities amenities = _unitOfWork.Amenities.Find(a => a.Name.ToLower() == ViewModel.Amenities.Name.ToLower()).FirstOrDefault();
                if (amenities != null)
                    throw new InvalidOperationException("Invalid, amenity with similar name already exist.");
                _unitOfWork.Amenities.Add(ViewModel.Amenities as Amenities);

                int n = await Task.Run(() => _unitOfWork.CompleteAsync());

                if (n == 0)
                    throw new InvalidOperationException("Could not add amenities.");

                _displayService.MessageDisplay($"Added amenities.\nName:{ViewModel.Amenities.Name}\nAffected rows:{n}", "Amenities added");
                ViewModel.Window.Close();
            }
            catch(ValidationException validationException)
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

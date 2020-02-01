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

namespace AvenueOne.ViewModels.Commands.RoomCommands
{
    public class RemoveRoomTypeCommand : ICommand
    {
        public IRoomTypeViewModel ViewModel { get; set; }
        private IUnitOfWork _unitOfWork;
        private IDisplayService _displayService;
        public RemoveRoomTypeCommand(IUnitOfWork unitOfWork, IDisplayService displayService)
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
                    throw new ArgumentNullException("ViewModel cannot be null");
                if (ViewModel.Model == null || ViewModel.ModelSelected == null)
                    throw new ArgumentNullException("Room type or selected cannot be null.");

                if (!ViewModel.Model.IsValid || !ViewModel.ModelSelected.IsValid)
                    throw new ValidationException("Invalid entry on room type or its selection.");

                RoomType roomType = await Task.Run(()=>_unitOfWork.RoomType.GetAsync(ViewModel.Model.Id)) ?? throw new InvalidOperationException("Room type selection does not exist");

                _unitOfWork.RoomType.Remove(roomType);
                int n = await Task.Run(() => _unitOfWork.CompleteAsync());
                if (n <= 0)
                    throw new InvalidOperationException("Could not remove room type.");
                _displayService.MessageDisplay($"Removed room type:\nName:{ViewModel.ModelSelected.Id}\nAffected rows:{n}");

            }
            catch (ValidationException valEx)
            {
                _displayService.ErrorDisplay(valEx.Message, "Invalid entry.");

            }
            catch (InvalidOperationException invalidOp)
            {
                _displayService.ErrorDisplay(invalidOp.Message, "Error on operation.");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

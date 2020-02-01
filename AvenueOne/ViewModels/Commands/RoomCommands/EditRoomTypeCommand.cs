using AvenueOne.Core.Models;
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
    public class EditRoomTypeCommand : ICommand
    {
        public IRoomTypeViewModel ViewModel { get; set; }
        private IUnitOfWork _unitOfWork;
        private IDisplayService _displayService;
        public EditRoomTypeCommand(IUnitOfWork unitOfWork, IDisplayService displayService)
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

        public async void Execute(object parameter)
        {
            try
            {
                if (ViewModel == null)
                    throw new ArgumentNullException("ViewModel cannot be null.");
                if (ViewModel.Model == null || ViewModel.ModelSelected == null)
                    throw new ArgumentNullException("Room type or selection cannot be null.");

                if (!ViewModel.Model.IsValid || !ViewModel.ModelSelected.IsValid)
                    throw new InvalidOperationException("Invalid entry on room type or selection.");

                RoomType roomType = _unitOfWork.RoomType.Get(ViewModel.ModelSelected.Id) ?? throw new InvalidOperationException("Could not find the room type.");
                ViewModel.ModelSelected.DeepCopyTo(roomType);

                int n = await Task.Run(() => _unitOfWork.CompleteAsync());
                if (n == 0)
                    throw new InvalidOperationException("Could not edit room type.");

                _displayService.MessageDisplay($"Edited:\nName:{ViewModel.ModelSelected.Id}\nAffected rows:{n}.");
            }
            catch(InvalidOperationException InvalidOp)
            {
                _displayService.ErrorDisplay(InvalidOp.Message, "Invalid operation.");
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}

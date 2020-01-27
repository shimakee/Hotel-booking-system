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
    public class EditRoomTypeCommand : BaseClassCommand, ICommand
    {
        public IRoomTypeViewModel ViewModel { get; set; }
        public EditRoomTypeCommand(IUnitOfWork unitOfWork, IDisplayService displayService)
            : base(unitOfWork, displayService)
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
                    throw new ArgumentNullException("ViewModel cannot be null.");
                if (ViewModel.RoomType == null || ViewModel.RoomTypeSelected == null)
                    throw new ArgumentNullException("Room type or selection cannot be null.");

                if (!ViewModel.RoomType.IsValid || !ViewModel.RoomTypeSelected.IsValid)
                    throw new InvalidOperationException("Invalid entry on room type or selection.");

                RoomType roomType = _unitOfWork.RoomType.Get(ViewModel.RoomType.Id) ?? throw new InvalidOperationException("Could not find the room type.");
                ViewModel.RoomTypeSelected.DeepCopyTo(roomType);

                int n = await Task.Run(() => _unitOfWork.CompleteAsync());
                if (n == 0)
                    throw new InvalidOperationException("Could not edit room type.");

                _displayService.MessageDisplay($"Edited:\nName:{ViewModel.RoomTypeSelected.Name}\nAffected rows:{n}.");
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

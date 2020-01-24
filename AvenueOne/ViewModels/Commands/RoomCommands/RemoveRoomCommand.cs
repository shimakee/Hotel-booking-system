using AvenueOne.Core;
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
    public class RemoveRoomCommand : ICommand
    {
        public IRoomViewModel ViewModel { get; set; }
        private IGenericUnitOfWork<Room> _unitOfWork;
        private IDisplayService _displayService;
        public RemoveRoomCommand(IGenericUnitOfWork<Room> unitOfWork, IDisplayService displayService)
        //public RemoveRoomCommand(IUnitOfWork unitOfWork, IDisplayService displayService)
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
                    throw new ArgumentNullException("View model cannot be null.");
                if (ViewModel.Room == null || ViewModel.RoomSelected == null)
                    throw new ArgumentNullException("Room or room selected cannot be null.");

                Room room = await Task.Run(() => _unitOfWork.Repositories[typeof(Room)].GetAsync(ViewModel.RoomSelected.Id)) ?? throw new InvalidOperationException("Room does not exist.");
                _unitOfWork.Repositories[typeof(Room)].Remove(room);
                int n = await Task.Run(() => _unitOfWork.CompleteAsync());
                if (n <= 0)
                    throw new InvalidOperationException("Could not remove.");
                _displayService.MessageDisplay($"Removed {ViewModel.RoomSelected.Name}.\nRows affected: {n}.");
            }
            catch(ArgumentNullException argEx)
            {
                _displayService.ErrorDisplay(argEx.Message, "Argument null error.");
            }
            catch(InvalidOperationException invalidEx)
            {
                _displayService.ErrorDisplay(invalidEx.Message, "Invalid Operation error.");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

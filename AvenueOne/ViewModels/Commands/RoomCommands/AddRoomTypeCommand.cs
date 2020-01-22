using AvenueOne.Core.Models;
using AvenueOne.Core.Models.Interfaces;
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
    public class AddRoomTypeCommand : BaseClassCommand, ICommand
    {
        public IRoomTypeWindowViewModel ViewModel;
        public AddRoomTypeCommand(IUnitOfWork unitOfWork, IDisplayService displaySerive)
            :base(unitOfWork, displaySerive)
        {
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (ViewModel != null)
                return this.ViewModel.UserAccount.IsAdmin;
            return false;
        }

        public async void Execute(object parameter)
        {
            try
            {
                if (ViewModel == null)
                    throw new ArgumentNullException("ViewModel cannot be null.");
                if (ViewModel.RoomType == null)
                    throw new ArgumentNullException("Room type cannot be null.");

                if (!ViewModel.RoomType.IsValid)
                    throw new ValidationException("Invalid entry on room type.");

                RoomType roomType = _unitOfWork.RoomType.Find(room => room.Name.ToLower() == ViewModel.RoomType.Name.ToLower()).FirstOrDefault();
                if (roomType != null)
                    throw new InvalidOperationException("Room type with similar name already exist.");
                _unitOfWork.RoomType.Add(ViewModel.RoomType as RoomType);

                int n = await Task.Run(() => _unitOfWork.CompleteAsync());

                if (n <= 0)
                    throw new InvalidOperationException("Could not add room type.");

                _displayService.MessageDisplay($"Added:\nName:{ViewModel.RoomType.Name}\nAffected rows:{n}.");
                ViewModel.Window.Close();
            }
            catch(ArgumentNullException argEx)
            {
                _displayService.ErrorDisplay(argEx.Message, "Invalid entry!");
            }
            catch(InvalidOperationException invalidEx)
            {
                _displayService.ErrorDisplay(invalidEx.Message, "Error on operation.");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

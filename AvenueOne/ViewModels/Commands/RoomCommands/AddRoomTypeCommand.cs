using AvenueOne.Core;
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
    public class AddRoomTypeCommand : BaseClassCommand<RoomType>, IBaseClassCommand<RoomType>
    {
        public AddRoomTypeCommand(IGenericUnitOfWork<RoomType> genericUnitOfWork, IDisplayService displayService)
            :base(genericUnitOfWork, displayService)
        {
            this._genericUnitOfWork = genericUnitOfWork;
            this._displayService = displayService;
        }

        public override async void Execute(object parameter)
        {
            try
            {
                if (ViewModel == null)
                    throw new ArgumentNullException("ViewModel cannot be null.");
                if (ViewModel.Model == null || ViewModel.ModelSelected == null)
                    throw new ArgumentNullException("Room type cannot be null.");

                if (!ViewModel.Model.IsValid || !ViewModel.ModelSelected.IsValid)
                    throw new ValidationException("Invalid entry on room type.");

                RoomType model = ViewModel.ModelSelected as RoomType;
                RoomType roomType = _genericUnitOfWork.Repositories[typeof(RoomType)].Find(room => room.Name.ToLower() == model.Name.ToLower()).FirstOrDefault();
                if(roomType != null)
                    throw new InvalidOperationException("Room type with similar name already exist.");

                //adding amenities
                if(model.Amenities != null && model.Amenities.Count > 0)
                {
                    List<Amenities> amenities = new List<Amenities>();
                    foreach (var item in model.Amenities)
                    {
                        if (!amenities.Contains(item))
                            amenities.Add(item);
                    }
                    model.Amenities = amenities;
                }
                //adding rooms
                if(model.Rooms != null && model.Rooms.Count >0)
                {
                    List<Room> rooms = new List<Room>();
                    foreach (var item in model.Rooms)
                    {
                        if (!rooms.Contains(item))
                            rooms.Add(item);
                    }
                    model.Rooms = rooms;
                }

                _genericUnitOfWork.Repositories[typeof(RoomType)].Add(ViewModel.ModelSelected as RoomType);

                int n = await Task.Run(() => _genericUnitOfWork.CompleteAsync());

                if (n <= 0)
                    throw new InvalidOperationException("Could not add room type.");

                _displayService.MessageDisplay($"Added:\nName:{model.Name}\nAffected rows:{n}.");
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

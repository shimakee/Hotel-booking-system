using AvenueOne.Core;
using AvenueOne.Core.Models;
using AvenueOne.Services.Interfaces;
using AvenueOne.ViewModels.Commands.ClassCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.Commands.RoomCommands
{
    public class CreateRoomTypeCommand : CreateClassCommand<RoomType>, IBaseClassCommand<RoomType>
    {
        public CreateRoomTypeCommand(IGenericUnitOfWork<RoomType> genericUnitOfWork, IDisplayService displayService)
            : base(genericUnitOfWork, displayService)
        {

        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        //public override async void Execute(object parameter)
        //{
        //    try
        //    {
        //        if (ViewModel == null)
        //            throw new ArgumentNullException("ViewModel cannot be null.");
        //            if (ViewModel.Model == null || ViewModel.ModelSelected == null)
        //                throw new ArgumentNullException("Room type cannot be null.");

        //            if (!ViewModel.Model.IsValid || !ViewModel.ModelSelected.IsValid)
        //                throw new InvalidOperationException("Invalid entry on room type.");



        //        if (n <= 0)
        //            throw new InvalidOperationException("Could not add room type.");

        //        _displayService.MessageDisplay($"Added:\nName:{model.Name}\nAffected rows:{n}.");
        //    }
        //    catch (ArgumentNullException argEx)
        //    {
        //        _displayService.ErrorDisplay(argEx.Message, "Argument null exception.");
        //    }
        //    catch (InvalidOperationException inEx)
        //    {
        //        _displayService.ErrorDisplay(inEx.Message, "Invalid operation exception.");
        //    }
        //    catch (Exception ex)
        //    {
        //        _displayService.ErrorDisplay(ex.Message, "Exception.");
        //    }
        //}

        protected override async Task<int> Insert()
        {
            RoomType model = ViewModel.ModelSelected as RoomType;
            RoomType roomType = _genericUnitOfWork.Repositories[typeof(RoomType)].Find(room => room.Name.ToLower() == model.Name.ToLower()).FirstOrDefault();
            if (roomType != null)
                throw new InvalidOperationException("Room type with similar name already exist.");

            //adding amenities
            if (model.Amenities != null && model.Amenities.Count > 0)
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
            if (model.Rooms != null && model.Rooms.Count > 0)
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

            return await Task.Run(() => _genericUnitOfWork.CompleteAsync());
        }
    }
}

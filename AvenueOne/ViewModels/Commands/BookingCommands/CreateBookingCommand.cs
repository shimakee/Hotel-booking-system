using AvenueOne.Core;
using AvenueOne.Core.Models;
using AvenueOne.Services.Interfaces;
using AvenueOne.ViewModels.Commands.ClassCommands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.Commands.BookingCommands
{
    public class CreateBookingCommand : CreateClassCommand<Booking>
    {
        public CreateBookingCommand(IGenericUnitOfWork<Booking> genericUnitOfWork, IDisplayService displayService)
            : base(genericUnitOfWork, displayService)
        {

        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override async void Execute(object parameter)
        {
            try
            {
                Validate();
                if (ViewModel.ModelSelected.Room == null)
                    throw new ValidationException("Must select a room.");
                if (ViewModel.ModelSelected.DateCheckin.Date < DateTime.Today.Date && !ViewModel.UserAccount.IsAdmin)
                    throw new InvalidOperationException("No authority to create booking before current date.");

                Booking model = ViewModel.ModelSelected;

                Booking booking = await Task.Run(() => _genericUnitOfWork.Repositories[typeof(Booking)]
                                                                                    .Find(b => model.DateCheckin >= b.DateCheckin && model.DateCheckin <= b.DateCheckout && model.Room.Id == b.Room.Id ||
                                                                                                b.DateCheckin >= model.DateCheckout && b.DateCheckout <= model.DateCheckout && model.Room.Id == b.Room.Id)
                                                                                    .FirstOrDefault());
                if (booking != null)
                    throw new InvalidOperationException("There is a booking conflict.");

                int n = await Insert();

                if (n <= 0)
                    throw new InvalidOperationException("Could not add model to database.");

                ViewModel.ClearClassCommand.Execute(null);
                _displayService.MessageDisplay($"Added {typeof(Booking)} model.\nId:{ViewModel.ModelSelected.Id}\nAffected rows:{n}", "Model added");
            }
            catch (ValidationException validationException)
            {
                _displayService.ErrorDisplay(validationException.Message, "Validation error!");
            }
            catch (DbUpdateException dbEx)
            {
                _displayService.ErrorDisplay(dbEx.InnerException.InnerException.Message, "Db error!");
            }
            catch (InvalidOperationException ex)
            {
                _displayService.ErrorDisplay(ex.Message, "Error on model insertion.");
            }
            catch (Exception ex)
            {
                //TODO: create logger
                ExceptionHandling(ex);
                throw;
            }
        }

    }
}

using AvenueOne.Core;
using AvenueOne.Core.Models;
using AvenueOne.Services.Interfaces;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvenueOne.ViewModels.Commands.BookingCommands
{
    public class AddBookingCommand : BaseClassCommand<Transaction>
    {
        public AddBookingCommand(IGenericUnitOfWork<Transaction> genericUnitOfWork, IDisplayService displayService)
            :base(genericUnitOfWork, displayService)
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
                ITransactionViewModel viewModel = ViewModel as ITransactionViewModel;
                if (viewModel.Model == null || viewModel.ModelSelected == null)
                    throw new InvalidOperationException("Transaction must be selected.");
                if (viewModel.BookingViewModel.ModelSelected == null || viewModel.Bookings == null)
                    throw new InvalidOperationException("There must ba a booking entry.");
                if (!viewModel.BookingViewModel.ModelSelected.IsValid)
                    throw new ValidationException("Booking must have a valid entry.");
                if(viewModel.BookingViewModel.ModelSelected.Room == null)
                    throw new ValidationException("Room must be selected.");

                foreach (var item in viewModel.BookingViewModel.ModelList)
                {
                    if (viewModel.BookingViewModel.ModelSelected.IsBookingConflict(item))
                        throw new InvalidOperationException("There is a conflict in booking");
                }

                foreach (var item in viewModel.Bookings)
                {
                    if(viewModel.BookingViewModel.ModelSelected.IsBookingConflict(item))
                        throw new InvalidOperationException("There is a conflict in booking");
                }


                Transaction transaction = await Task.Run(() => _genericUnitOfWork.Repositories[typeof(Transaction)].GetAsync(viewModel.Model.Id));
                if(transaction != null)
                {
                    transaction.Bookings.Add(viewModel.BookingViewModel.ModelSelected);
                    //ViewModel.ModelSelected = transaction;
                }
                else
                {
                    if (!ViewModel.ModelSelected.IsValid || !viewModel.BookingViewModel.ModelSelected.IsValid)
                        throw new ValidationException("Transaction must have a valid booking entry.");
                    if (ViewModel.ModelSelected.Customer == null)
                        throw new ValidationException("Customer must be selected.");

                    viewModel.Bookings.Add(viewModel.BookingViewModel.ModelSelected);
                    //Transaction modelSelected = ViewModel.ModelSelected;
                    _genericUnitOfWork.Repositories[typeof(Transaction)].Add(ViewModel.ModelSelected);
                    //ViewModel.ModelSelected = modelSelected;
                }

                int n = await Task.Run(() => _genericUnitOfWork.CompleteAsync());

                viewModel.BookingViewModel.ClearClassCommand.Execute(null);
                viewModel.BookingViewModel.RoomsAvailable = viewModel.GetAvailableRooms();
            }
            //catch (DbEntityValidationException enEx)
            //{
            //    _displayService.MessageDisplay(enEx.Message, "Validation exception.");
            //    _genericUnitOfWork.Repositories[typeof(Transaction)].Remove(ViewModel.ModelSelected);
            //}
            catch (ValidationException valEx)
            {
                _displayService.MessageDisplay(valEx.Message, "Validation exception.");
            }
            catch(InvalidOperationException inEx)
            {
                _displayService.MessageDisplay(inEx.Message, "Invalid operation exception.");
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}

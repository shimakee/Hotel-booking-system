using AvenueOne.Core;
using AvenueOne.Interfaces;
using AvenueOne.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.Commands.ClassCommands
{
    public class DeleteClassCommand<T> : BaseClassCommand<T> where T : class, IBaseObservableModel<T>, new()
    {
        public DeleteClassCommand(IGenericUnitOfWork<T> genericUnitOfWork, IDisplayService displayService)
            :base(genericUnitOfWork, displayService)
        {

        }
        public override async void Execute(object parameter)
        {
            try
            {
                base.Validate();
                string id = ViewModel.Model.Id;

                bool ConfirmDelete = _displayService.MessagePrompt($"Are you sure you want to delete? delete action will also cascade to all reference objects.", "Delete object");

                if (ConfirmDelete)
                {
                    int n = await Delete();
                    if (n <= 0)
                        throw new InvalidOperationException("Could not delete model from database.");

                    //modelselected not be null after delete
                    ViewModel.ClearClassCommand.Execute(null);

                    _displayService.MessageDisplay($"Deleted {typeof(T)} model.\nId:{id}\nAffected rows:{n}", "Model deleted");
                }
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

        protected virtual async Task<int> Delete()
        {
            T model = await Task.Run(() => _genericUnitOfWork.Repositories[typeof(T)].Get(ViewModel.Model.Id));
            if (model == null)
                throw new InvalidOperationException("Invalid, model does not exist.");

            _genericUnitOfWork.Repositories[typeof(T)].Remove(model);

            return await Task.Run(() => _genericUnitOfWork.CompleteAsync());
        }
    }
}

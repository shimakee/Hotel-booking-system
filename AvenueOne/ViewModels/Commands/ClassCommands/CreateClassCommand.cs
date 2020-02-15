using AvenueOne.Core;
using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.Commands.ClassCommands
{
    public class CreateClassCommand<T> : BaseClassCommand<T> where T : class, IBaseObservableModel<T>, new()
    {
        public CreateClassCommand(IGenericUnitOfWork<T> genericUnitOfWork, IDisplayService displayService)
            : base(genericUnitOfWork, displayService)
        {
        }

        public override async void Execute(object parameter)
        {
            try
            {

                base.Validate();
                int n = await Insert();
                if (n <= 0)
                    throw new InvalidOperationException("Could not add model to database.");

                ViewModel.ClearClassCommand.Execute(null);
                _displayService.MessageDisplay($"Added {typeof(T)} model.\nId:{ViewModel.ModelSelected.Id}\nAffected rows:{n}", "Model added");
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

        protected virtual async Task<int>Insert()
        {
            T model = await Task.Run(() => _genericUnitOfWork.Repositories[typeof(T)].Get(ViewModel.ModelSelected.Id));
            if (model != null)
                throw new InvalidOperationException("Invalid, model with similar property values already exist.");
            _genericUnitOfWork.Repositories[typeof(T)].Add(ViewModel.ModelSelected as T);

            return await Task.Run(() => _genericUnitOfWork.CompleteAsync());
        }
    }
}

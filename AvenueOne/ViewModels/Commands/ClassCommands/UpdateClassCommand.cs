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
    public class UpdateClassCommand<T> : BaseClassCommand<T> where T : class, IBaseObservableModel<T>, new()
    {
        public UpdateClassCommand(IGenericUnitOfWork<T> genericUnitOfWork, IDisplayService displayService)
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
                base.Validate();
                PreUpdate();
                int n = await Update();
                if (n == 0)
                    throw new InvalidOperationException("Could not update model.");

                ViewModel.ClearClassCommand.Execute(null);
                _displayService.MessageDisplay($"Updated {typeof(T)} model.\nId:{ViewModel.Model.Id}\nAffected rows:{n}", "Model updated");
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

        protected virtual async Task<int> Update()
        {
            T model = await Task.Run(() => _genericUnitOfWork.Repositories[typeof(T)].Get(ViewModel.Model.Id));
            if (model == null)
                throw new InvalidOperationException("Invalid, model does not exist.");
            ViewModel.ModelSelected.Id = model.Id;
            ViewModel.ModelSelected.DateAdded = model.DateAdded;
            ViewModel.ModelSelected.DeepCopyTo(model);

            return await Task.Run(() => _genericUnitOfWork.CompleteAsync());
        }

        protected virtual void PreUpdate()
        {
            ViewModel.ModelSelected.DateModified = DateTime.Now;
        }
    }
}

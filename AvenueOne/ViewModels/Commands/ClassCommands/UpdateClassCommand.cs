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

        public override async void Execute(object parameter)
        {
            try
            {
                if (this.ViewModel == null)
                    throw new NullReferenceException("Viewmodel cannot be null.");
                if (this.ViewModel.Model == null || this.ViewModel.ModelSelected == null)
                    throw new NullReferenceException("Model or Selection cannot be null.");

                if (!ViewModel.Model.IsValid || !ViewModel.ModelSelected.IsValid)
                    throw new ValidationException("Invalid input on Model or ModelSelected.");

                T model = await Task.Run(() => _genericUnitOfWork.Repositories[typeof(T)].Get(ViewModel.Model.Id));
                if (model == null)
                    throw new InvalidOperationException("Invalid, model does not exist.");
                ViewModel.ModelSelected.Id = model.Id;
                ViewModel.ModelSelected.DeepCopyTo(model);

                int n = await Task.Run(() => _genericUnitOfWork.CompleteAsync());

                if (n == 0)
                    throw new InvalidOperationException("Could not update model.");

                _displayService.MessageDisplay($"Updated {typeof(T)} model.\nId:{ViewModel.Model.Id}\nAffected rows:{n}", "Model updated");
                //ViewModel.Window.Close();
            }
            catch (DbUpdateException dbEx)
            {
                _displayService.ErrorDisplay(dbEx.InnerException.InnerException.Message, "Db error!");
            }
            catch (ValidationException validationException)
            {
                _displayService.ErrorDisplay(validationException.Message, "Validation error!");
            }
            catch (InvalidOperationException ex)
            {
                _displayService.ErrorDisplay(ex.Message, "Error on model insertion.");
            }
            catch (Exception exception)
            {
                //TODO: create logger
                throw;
            }
        }
    }
}

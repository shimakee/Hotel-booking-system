using AvenueOne.Core;
using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
                if (this.ViewModel == null)
                    throw new NullReferenceException("Viewmodel cannot be null.");
                if (this.ViewModel.Model == null || this.ViewModel.ModelSelected == null)
                    throw new NullReferenceException("Model or Selection cannot be null.");

                if (!ViewModel.Model.IsValid || !ViewModel.ModelSelected.IsValid)
                    throw new ValidationException("Invalid input on Model or ModelSelected.");
                //T model = await Task.Run(()=> _genericUnitOfWork.Repositories[typeof(T)].GetAsync(ViewModel.ModelSelected.Id));
                T model = await Task.Run(()=> _genericUnitOfWork.Repositories[typeof(T)].Find(m=> m.Equals(ViewModel.Model)).FirstOrDefault());
                //or use find with model.equals method
                if (model != null)
                    throw new InvalidOperationException("Invalid, model with similar property values already exist.");
                _genericUnitOfWork.Repositories[typeof(T)].Add(ViewModel.ModelSelected as T);

                int n = await Task.Run(() => _genericUnitOfWork.CompleteAsync());

                if (n == 0)
                    throw new InvalidOperationException("Could not add model to database.");

                _displayService.MessageDisplay($"Added {typeof(T)} model.\nId:{ViewModel.ModelSelected.Id}\nAffected rows:{n}", "Model added");
                //ViewModel.Window.Close();
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

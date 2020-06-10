using AvenueOne.Core;
using AvenueOne.Models;
using AvenueOne.Services.Interfaces;
using AvenueOne.ViewModels.Commands.ClassCommands;
using AvenueOne.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AvenueOne.ViewModels.Commands.UserCommands
{
    public class DeleteUserCommand : DeleteClassCommand<User>, IBaseClassCommand<User>
    {

        public DeleteUserCommand(IGenericUnitOfWork<User> genericUnitOfWork, IDisplayService displayService)
            : base(genericUnitOfWork, displayService)
        {

        }

        public override async void Execute(object parameter)
        {
            try
            {
                Validate();
                string id = ViewModel.Model.Id;

                bool ConfirmDelete = _displayService.MessagePrompt($"Are you sure you want to delete? delete action will also cascade to all reference objects.", "Delete object");

                List<User> adminUsers = await Task.Run(() => _genericUnitOfWork.Repositories[typeof(User)].Find(u => u.IsAdmin == true).ToList());
                if (adminUsers.Count <= 1 && ViewModel.Model.IsAdmin == true)
                    throw new InvalidOperationException("Could not delete user, there must be atleast 1 admin account.");

                bool confirmDeleteOwnAccount = false;
                if (ViewModel.UserAccount.Id == ViewModel.Model.Id)
                {
                    confirmDeleteOwnAccount = _displayService.MessagePrompt($"You are deleting your own account, proceed?", "Delete own account.");
                    if (!confirmDeleteOwnAccount)
                        return;
                }

                if (ConfirmDelete)
                {
                    int n = await Delete();
                    if (n <= 0)
                        throw new InvalidOperationException("Could not delete model from database.");

                    //modelselected not be null after delete
                    ViewModel.ClearClassCommand.Execute(null);

                    _displayService.MessageDisplay($"Deleted {typeof(User)} model.\nId:{id}\nAffected rows:{n}", "Model deleted");

                    if (confirmDeleteOwnAccount) //logout when deleteing self account - by closing main window and opening loginwindow
                    {
                        LoginWindow loginWindow = new LoginWindow();
                        loginWindow.Show();
                        ViewModel.Window.Close();
                    }
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
                _displayService.ErrorDisplay(ex.Message, "Error on model deletion.");
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

using AvenueOne.Core;
using AvenueOne.Interfaces;
using AvenueOne.Models;
using AvenueOne.Services;
using AvenueOne.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AvenueOne.ViewModels.Commands.UserCommands
{
    public class UpdateUserCommand : BaseClassCommand<User>, IBaseClassCommand<User>
    {
        public UpdateUserCommand(IGenericUnitOfWork<User> genericUnitOfWork, IDisplayService displayService)
            : base(genericUnitOfWork, displayService)
        {

        }

        public override async void Execute(object parameter)
        {
            try
            {
                if (ViewModel == null)
                    throw new NullReferenceException("View model must not be null in order to execute command.");
                if (ViewModel.Model == null || ViewModel.ModelSelected == null)
                        throw new NullReferenceException("No item selected to update.");
                if (ViewModel.ModelSelected.Person == null)
                        throw new NullReferenceException("No profile to update.");


                if (!ViewModel.ModelSelected.IsValid || !ViewModel.ModelSelected.Person.IsValid)
                    throw new InvalidOperationException("Invalid entry in user account.");
                
                

                //get parameters
                object[] values = (object[])parameter ?? throw new NullReferenceException("parameter cannot be null, you need to pass password and password confirmbox");

                CheckBox IsPasswordIncludedCheckBox = (CheckBox)values[0];
                PasswordBox passwordBox = (PasswordBox)values[1];
                PasswordBox passwordConfirmBox = (PasswordBox)values[2];

                //is password included in update
                bool IsPasswordIncluded = IsPasswordIncludedCheckBox.IsChecked.GetValueOrDefault();
                //get password
                ViewModel.ModelSelected.Password = passwordBox.Password;
                ViewModel.ModelSelected.PasswordConfirm = passwordConfirmBox.Password;

                User user = await Task.Run(() => _genericUnitOfWork.Repositories[typeof(User)].GetAsync(ViewModel.Model.Id)) ?? throw new NullReferenceException("Account does not exist.");

                user.Username = ViewModel.ModelSelected.Username;
                ViewModel.ModelSelected.Person.Id = user.Person.Id;
                ViewModel.ModelSelected.Person.User = user;
                ViewModel.ModelSelected.Person.DeepCopyTo(user.Person);

                if (IsPasswordIncluded)
                {
                    if (!ViewModel.ModelSelected.IsValidProperty("Password"))
                        throw new InvalidOperationException("Invalid entry on password.");
                    if (!ViewModel.ModelSelected.IsValidProperty("PasswordConfirm"))
                        throw new InvalidOperationException("Invalid entry on password confirmation.");

                    user.Password = HashService.Hash(ViewModel.ModelSelected.Password);
                    user.PasswordConfirm = user.Password;
                }

                int n = await Task.Run(() => _genericUnitOfWork.CompleteAsync());

                if (n <= 0)
                    throw new InvalidOperationException("Could not edit accout.");
                _displayService.MessageDisplay($"Edited account: {ViewModel.Model.Username}.\nAffected rows: {n}.");
            }
            catch(NullReferenceException nullEx)
            {
                _displayService.ErrorDisplay(nullEx.Message, "Null reference exception.");
            }
            catch(InvalidOperationException inEx)
            {
                _displayService.ErrorDisplay(inEx.Message, "Invalid operation exception.");
            }
            catch (Exception ex)
            {
                _displayService.ErrorDisplay(ex.Message, "Error");
                throw;
            }
        }
    }
}

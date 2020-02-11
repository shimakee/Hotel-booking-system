using AvenueOne.Core;
using AvenueOne.Interfaces;
using AvenueOne.Models;
using AvenueOne.Services;
using AvenueOne.Services.Interfaces;
using AvenueOne.ViewModels.Commands.ClassCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AvenueOne.ViewModels.Commands.UserCommands
{
    public class CreateUserCommand : CreateClassCommand<User>, IBaseClassCommand<User>
    {
        public CreateUserCommand(IGenericUnitOfWork<User> genericUnitOfWork, IDisplayService displayService)
            :base(genericUnitOfWork, displayService)
        {

        }

        public override async void Execute(object parameter)
        {
            try
            {
                if (parameter == null)
                    throw new NullReferenceException("parameter passed must not be null.");

                //get parameters
                object[] values = (object[])parameter;

                CheckBox IsPasswordIncludedCheckBox = (CheckBox)values[0];
                bool IsPasswordIncluded = IsPasswordIncludedCheckBox.IsChecked.GetValueOrDefault();

                PasswordBox passwordPasswordBox = (PasswordBox)values[1];
                string password = passwordPasswordBox.Password;

                PasswordBox passwordConfirmPasswordBox = (PasswordBox)values[2];
                string passwordConfirm = passwordConfirmPasswordBox.Password;


                if (password == null || passwordConfirm == null)
                    throw new NullReferenceException("Password and PasswordConfirm cannot be null.");

                Validate();

                ViewModel.ModelSelected.Password = password;
                ViewModel.ModelSelected.PasswordConfirm = passwordConfirm;

                int n = await Insert();

                if (n <= 0)
                    throw new InvalidOperationException("Could not add user.");

                _displayService.MessageDisplay($"Added accoun:\n\nUsername: {ViewModel.ModelSelected.Username}\nName:{ViewModel.ModelSelected.Person.FullName}\n\nAffected rows:{n}.",
                                                "Account added.");
            }
            catch (ArgumentNullException argEx)
            {
                _displayService.ErrorDisplay(argEx.Message, "Argument null Exception.");
            }
            catch (NullReferenceException nullEx)
            {
                _displayService.ErrorDisplay(nullEx.Message, "Null reference Exception.");
            }
            catch(InvalidOperationException inEx)
            {
                _displayService.ErrorDisplay(inEx.Message, "Invalid Operation Execption.");
            }
            catch (Exception ex)
            {
                _displayService.ErrorDisplay(ex.Message, "Error");
                throw;
            }
        }

        protected override async Task<int> Insert()
        {
            User user = ViewModel.ModelSelected;
            user.Person = ViewModel.ModelSelected.Person;

            if (user == null || user.Person == null)
                throw new ArgumentNullException("User and person view model cannot be null.");

            if (!user.Person.IsValid)
                throw new InvalidOperationException("Invalid profile detais.");
            if (!user.IsValid)
                throw new InvalidOperationException("Invalid user details.");
            if (!user.IsValidProperty("Password"))
                throw new InvalidOperationException("Invalid password");
            if (!user.IsValidProperty("PasswordConfirm"))
                throw new InvalidOperationException("Invalid password");

            byte[] salt = HashService.CreateSalt();
            user.Password = HashService.Hash(user.Password, salt);
            user.PasswordConfirm = user.Password;

            User user2 = await Task.Run(() => _genericUnitOfWork.Repositories[typeof(User)].Find(u => u.Username == user.Username).FirstOrDefault());
            if (user2 != null)
                throw new InvalidOperationException("Username already exist.");

            _genericUnitOfWork.Repositories[typeof(User)].Add(user);
            return await Task.Run(() => _genericUnitOfWork.CompleteAsync());
        }  
    }
}

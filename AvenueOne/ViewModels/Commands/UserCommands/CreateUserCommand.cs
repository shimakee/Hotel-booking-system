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
    public class CreateUserCommand : BaseClassCommand<User>, IBaseClassCommand<User>
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
                if (ViewModel.Model == null || ViewModel.ModelSelected == null)
                    throw new NullReferenceException("User cannot be null");
                if (ViewModel.ModelSelected.Person == null)
                    throw new NullReferenceException("Profile cannot be null.");


                IUser User = ViewModel.ModelSelected;
                User.Person = ViewModel.ModelSelected.Person;

                User.Password = password;
                User.PasswordConfirm = passwordConfirm;

                int n = await AddUser(User);

                if (n <= 0)
                    throw new InvalidOperationException("Could not add user.");

                _displayService.MessageDisplay($"Added accoun:\n\nUsername: {User.Username}\nName:{User.Person.FullName}\n\nAffected rows:{n}.",
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

        private async Task<int> AddUser(IUser User)
        {
            if (User == null || User.Person == null)
                throw new ArgumentNullException("User and person view model cannot be null.");

            if (!User.Person.IsValid)
                throw new ArgumentException("Invalid profile detais.");
            if (!User.IsValid)
                throw new ArgumentException("Invalid user details.");
            if (!User.IsValidProperty("Password"))
                throw new ArgumentException("Invalid password");
            if (!User.IsValidProperty("PasswordConfirm"))
                throw new ArgumentException("Invalid password");

            byte[] salt = HashService.CreateSalt();
            User.Password = HashService.Hash(User.Password, salt);
            User.PasswordConfirm = User.Password;
            User user = User as User;
            _genericUnitOfWork.Repositories[typeof(User)].Add(user);
            int n = await Task.Run(() => _genericUnitOfWork.CompleteAsync());

            return n;
        }
    }
}

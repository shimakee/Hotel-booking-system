using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Models;
using AvenueOne.Properties;
using AvenueOne.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Utilities
{
    public class LoginService : ILoginService
    {
        private IUnitOfWork _unitOfWork;

        public LoginService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User Login(IUser user)
        {
            if (user == null)
                throw new ArgumentNullException("User object cannot be null.");

            return Login(user.Username, user.Password);
        }
        public User Login(string username, string password)
        {
            if (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("The argument username and password cannot be null, empty, or whitespace.");

            User user = _unitOfWork.Users.Find(u => u.Username == username).FirstOrDefault<User>();

            //check that password matches
            if (user == null)
                return null;
            if (!HashService.Verify(password, user.Password))
                return null;

            user.Password = null; //remove password so that it wont be shown.
            return user;
        }

        public bool IsValidLogin(IUser user)
        {
            if (user == null)
                throw new ArgumentNullException("User cannot be null.");

            IUser userToCheck = Login(user.Username, user.Password);
            if (userToCheck == null)
                return false;
            return true;

        }
        public bool IsValidLogin(string username, string password)
        {
            //validate args
            if (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("The argument username and password cannot be null, empty, or whitespace.");

            IUser user = Login(username, password);

            if (user == null)
                return false;
            return true;
        }
    }
}

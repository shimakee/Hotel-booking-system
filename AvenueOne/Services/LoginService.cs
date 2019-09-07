using AvenueOne.Interfaces;
using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Models;
using AvenueOne.Properties;
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

        public bool Login(string username, string password)
        {
            if (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("The argument username and password cannot be null, empty, or whitespace.");

            //IUser user = _unitOfWork.Users.Find(u => u.Username == username).Single;
            IUser user = _unitOfWork.Users.Find(u => u.Username == username).FirstOrDefault<User>();

            if (user == null)
                return false;

            if (user.Password != password)
                return false;

            user.Password = null;
            Settings.Default["UserAccount"] = user;
            Settings.Default.Save();

            return true;
        }

        public IUser GetValidatedDetails(string username, string password)
        {
            //validate args
            if (String.IsNullOrEmpty(username) || String.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException("The argument username cannot be null, empty, or whitespace.");

            if (String.IsNullOrEmpty(password) || String.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("The argument password cannot be null, empty, or whitespace.");

            ////connect to databse

            ////query details here
            //IUser userToFind = new User(username, password);
            //IUser doesExist = _unitOfWork.Users.Find(u => u.Equals(userToFind)).Single();

            ////check that password matches
            ////TODO -  just have amethod in unit of work user repository or something
            //if  (doesExist != null && password == doesExist.Password)
            //    return doesExist;

            return new User(username, password, true);
        }

        public bool IsValidLogin(string username, string password)
        {
            //validate args
            if (String.IsNullOrEmpty(username) || String.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException("The argument username cannot be null, empty, or whitespace.");

            if (String.IsNullOrEmpty(password) || String.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("The argument password cannot be null, empty, or whitespace.");

            IUser user = GetValidatedDetails(username, password);

            if (user == null)
                return false;
            return true;
        }

        public bool DoesUsernameExist(string username)
        {
            //validate args
            if (String.IsNullOrEmpty(username) || String.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException("The argument username cannot be null, empty, or whitespace.");

            return true;
            //connect to databse


            //query for username
            //IUser userToFind = new User(username);
            //IUser doesExist = _unitOfWork.Users.Find(user => user.Equals(userToFind));

            //if (doesExist != null)
            //    return true;
            //return false;
        }
    }
}

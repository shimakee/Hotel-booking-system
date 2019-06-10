using AvenueOne.Interfaces;
using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Utilities
{
    public class LoginProcessor : ILoginProcessor
    {

        
        public IUserModel GetValidatedDetails(string username, string password)
        {
            //validate args
            if (String.IsNullOrEmpty(username) || String.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException("The argument username cannot be null, empty, or whitespace.");

            if (String.IsNullOrEmpty(password) || String.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("The argument password cannot be null, empty, or whitespace.");

            //connect to databse

            //query details here
            bool doesExist = true;

            //check that password matches
            bool isValidPassword = true;

            if (doesExist && isValidPassword)
                return new UserModel("shimakee", "secret", true);
            return null;
        }

        public bool IsValidLogin(string username, string password)
        {
            IUserModel user = GetValidatedDetails(username, password);

            if (user == null)
                return false;
            return true;
        }

        public bool DoesUsernameExist(string username)
        {
            //validate args
            if (String.IsNullOrEmpty(username) || String.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException("The argument username cannot be null, empty, or whitespace.");

            //connect to databse

            //query for username
            bool doesExist = true;

            if (doesExist)
                return true;
            return false;
        }
    }
}

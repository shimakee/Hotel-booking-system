using AvenueOne.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Utilities
{
    public class ValidatorUserModel : IUserModelValidator
    {
        public bool HasContent(string word)
        {
            if (String.IsNullOrEmpty(word) || String.IsNullOrWhiteSpace(word))
                return false;
            return true;
        }

        public bool ValidateUserModel(IUserModel userModel)
        {
            bool hasValidId = ValidateId(userModel.Id);
            bool hasValidUsername = ValidateUsername(userModel.Username);
            bool hasValidPassword = ValidatePassword(userModel.Password);

            if(hasValidId && hasValidUsername && hasValidPassword)
                return true;
            return false;
        }

        public bool HasId(string Id)
        {
            if(HasContent(Id))
                return true;
            return false;
        }

        public bool ValidateId(string Id)
        {
            if(!HasContent(Id))
                return false;

            //regex validation here
            return true;
        }

        //public bool ValidateId(string Id, string regex)
        //{
        //    if (!HasContent(Id))
        //        return false;

        //    //regex validation here
        //    return true;
        //}

        public bool ValidateUserPass(string username, string password)
        {
            // some regex validation here

            if (this.HasContent(username) && this.HasContent(password))
                return true;
            return false;
        }

        //public bool ValidateUserPass(string username, string password, string regex)
        //{
        //    // custom regex validation here

        //    if (this.HasContent(username) && this.HasContent(password))
        //        return true;
        //    return false;
        //}

        public bool ValidateUsername(string username)
        {
            //some regex validation here

            return this.HasContent(username);
        }

        //public bool ValidateUsername(string username, string regex)
        //{
        //    //customized regex validation here

        //    return this.HasContent(username);
        //}

        public bool ValidatePassword(string password)
        {
            //some regex validation here

            return this.HasContent(password);
        }

        //public bool ValidatePassword(string password, string regex)
        //{
        //    //customized regex expression here

        //    return this.HasContent(password);
        //}


    }
}

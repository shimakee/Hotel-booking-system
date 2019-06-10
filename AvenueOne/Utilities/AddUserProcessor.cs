using AvenueOne.Interfaces;
using AvenueOne.Utilities.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Utilities
{
    public class AddUserProcessor : IAddUserProcessor
    {
        public IUserModel AddUser(IUserModel userModel)
        {
            if (userModel == null)
                throw new ArgumentNullException("Usermodel cannot be null.");
            if(userModel.Username == null)
                throw new ArgumentNullException("Username cannot be null.");
            if (userModel.Password == null)
                throw new ArgumentNullException("Password cannot be null.");

            //generate user ID - alternate action instead of throwig arg exception
            if (userModel.Id == null)
                userModel.Id = Generator.GenerateId();

            //connect to database

            //check if user exist
            bool doesExist = false;

            if (doesExist)
                throw new ArgumentException("Cannot add a user that already exists.");

            //add user
            bool hasAddedUser = true;

            //successfully added user
            if(hasAddedUser)
                return userModel;
            return null; 
        }
    }
}

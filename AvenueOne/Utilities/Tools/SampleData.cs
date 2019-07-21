using AvenueOne.Interfaces;
using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Utilities.Tools
{
    class SampleData
    {
        public static IEnumerable<IUser> GetUsersList()
        {
            IList<IUser> users = new List<IUser>()
            {
                new UserModel("shimakee", "shimakee", true),
                new UserModel("ken", "ken"),
                new UserModel("dinah", "dinah", true),
                new UserModel("kristof", "kristof"),
                new UserModel("kenndi", "kenndi"),
                new UserModel("kenneth", "kenneth")
            };

            return users;
        }
    }
}

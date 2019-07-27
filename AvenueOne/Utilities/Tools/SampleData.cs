using AvenueOne.Interfaces;
using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Utilities.Tools
{
    class SampleData
    {
        private static SampleData _instance = null;

        private List<IUser> _usersList;

        private SampleData()
        {
            _usersList = new List<IUser>()
            {
                new UserModel("shimakee", "shimakee", true),
                new UserModel("ken", "ken"),
                new UserModel("dinah", "dinah", true),
                new UserModel("kristof", "kristof"),
                new UserModel("kenndi", "kenndi"),
                new UserModel("kenneth", "kenneth")
            };
            
        }

        public static SampleData SingeInstance
        {
            get
            {
                if (_instance == null)
                    _instance = new SampleData();

                return _instance;
            }


        }

        public List<IUser> Users
        {
            get
            {
                return _usersList;
            }
        }

    }
}

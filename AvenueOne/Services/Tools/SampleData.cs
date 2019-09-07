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
        private List<IPerson> _personList;

        private SampleData()
        {
            IPerson Ken = new Person() { FirstName = "Kenneth", LastName = "De Leon", CivilStatus = CivilStatusType.Married, Gender = GenderType.Male};
            IPerson Dinah = new Person() { FirstName = "Dinah Joy", LastName = "De Leon", MaidenName = "Hong", CivilStatus = CivilStatusType.Married, Gender = GenderType.Female };
            IPerson Tof = new Person() { FirstName = "Kenneth", LastName = "De Leon", CivilStatus = CivilStatusType.Single, Gender = GenderType.Male};
            IUser ken = new User("shimakee", "shimakee", true);
            IUser dinah = new User("dinah", "dinah", true);
            IUser tof = new User("kristof", "kristof");

            _usersList = new List<IUser>()
            {
                ken, dinah, tof,
                new User("kenndi", "kenndi"),
                new User("kenneth", "kenneth")
            };

            _personList = new List<IPerson>()
            {
                Ken, Dinah, Tof
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

        public List<IPerson> Persons
        {
            get
            {
                return _personList;
            }
        }

    }
}

using AvenueOne.Interfaces;
using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Models
{
    public class Customer : BaseObservableModel, ICustomer
    {
        private string _id;
        private Person _person;

        #region Properties

        [Required]
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }



        #endregion


        #region Constructors

        public Customer()
        {
            this.Id=GenerateId();
        }

        public Customer(string id)
        {
            this.Id = id;
        }

        #endregion

        #region Reference
        public virtual Person Person
        {
            get { return _person; }
            set
            {
                _person = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Customer))
                return false;

            return this.Id == ((Customer)obj).Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}

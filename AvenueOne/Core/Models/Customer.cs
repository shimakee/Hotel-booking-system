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
    public class Customer : BaseObservableModel<Customer>, ICustomer
    {
        #region Reference

        private Person _person;
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

        #region Constructors

        public Customer()
            :base()
        {
        }

        public Customer(string id)
        {
            this.Id = id;
        }

        #endregion

        #region Overrides

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
        #endregion

    }
}

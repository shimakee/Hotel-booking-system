using AvenueOne.Interfaces.ViewModelInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.ModelViewModel
{
    public abstract class ModelViewModel : IModelViewModel
    {
        public Dictionary<string, string> ErrorCollection { get; private set; }

        public ModelViewModel()
        {
            ErrorCollection = new Dictionary<string, string>();
        }

        #region IDataErrorInfo
        string IDataErrorInfo.Error
        {
            get
            {
                return null;
            }
        }

        string IDataErrorInfo.this[string property]
        {
            get
            {
                return ValidateProperty(property);
            }
        }
        #endregion

        #region Property Changed
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] String property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion

        #region Validation
        public bool IsValid
        {
            get
            {
                foreach (KeyValuePair<string, string> error in ErrorCollection)
                {
                    if (error.Value != null)
                        return false;
                }

                return true;
            }
        }

        //public void ValidateAllProperties()
        //{
        //    ValidationContext context = new ValidationContext(this);
        //    PropertyInfo[] properties = this.GetType().GetProperties();

        //    foreach (PropertyInfo property in properties)
        //    {
        //        ValidateProperty(property.ToString());
        //    }

        //}



        public bool IsValidProperty(string property)
        {
            string result = ValidateProperty(property);
            if (result == null)
                return true;
            return false;
        }

        private string ValidateProperty(string property)
        {
            var context = new ValidationContext(this, null, null) { MemberName = property };
            var result = new List<ValidationResult>();
            var instanceProperty = this.GetType().GetProperty(property);

            if (instanceProperty == null)
                throw new NullReferenceException("Property referenced cannot be null.");

            var value =instanceProperty.GetValue(this);
            string errorMessage = null;

            bool isValid = Validator.TryValidateProperty(value, context, result);
            if (!isValid)
                errorMessage = result.First().ErrorMessage;

            if (ErrorCollection.ContainsKey(property))
            {
                ErrorCollection[property] = errorMessage;
            }
            else if (errorMessage != null)
            {
                ErrorCollection.Add(property, errorMessage);
            }

            OnPropertyChanged("ErrorCollection");
            return errorMessage;
        }
        #endregion
    }
}

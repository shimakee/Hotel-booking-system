using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Interfaces
{
    public interface IBaseObservableModel<T> : IDataErrorInfo, INotifyPropertyChanged where T : IBaseObservableModel<T>, new ()
    {
        //Dictionary<string, string> ErrorCollection { get; }
        string Id { get; set; }
        DateTime DateAdded { get; set; }
        DateTime? DateModified { get; set; }
        bool IsValid { get; }
        bool IsValidProperty(string property);
        T Copy();
        T CopyTo(T model);
        T DeepCopy();
        T DeepCopyTo(T model);
    }
}

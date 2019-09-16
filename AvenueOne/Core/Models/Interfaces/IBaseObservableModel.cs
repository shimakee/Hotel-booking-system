using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Interfaces
{
    public interface IBaseObservableModel : IDataErrorInfo, INotifyPropertyChanged
    {
        //Dictionary<string, string> ErrorCollection { get; }
        bool IsValid { get; }
        bool IsValidProperty(string property);
    }
}

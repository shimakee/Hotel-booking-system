using AvenueOne.Interfaces;
using AvenueOne.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace AvenueOne.ViewModels.WindowsViewModels.Interfaces
{
    public interface IMainWindowViewModel : IWindowViewModel
    {
        PlutoContext Context { get; }
        Dictionary<string, Page> Pages { get; }


    }
}

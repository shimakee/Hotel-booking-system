using AvenueOne.Interfaces;
using AvenueOne.Persistence.Repositories;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using AvenueOne.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AvenueOne.ViewModels.WindowsViewModels
{
    public class MainWindowViewModel : WindowViewModel, IMainWindowViewModel
    {
        public PlutoContext Context { get; private set; }
        public Dictionary<string, Page> Pages { get;  private set; }

        public MainWindowViewModel(Window window, PlutoContext context)
            :base(window)
        {
            if (window == null || context == null)
                throw new ArgumentNullException("Window or context cannot be null.");

            this.Context = context;
            this.Pages = new Dictionary<string, Page>();
           this.Pages.Add("AdminPage", new AdminPage(Context));
        }

    }
}

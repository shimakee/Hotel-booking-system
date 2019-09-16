using AvenueOne.Interfaces;
using AvenueOne.Models;
using AvenueOne.Persistence.Repositories;
using AvenueOne.Properties;
using AvenueOne.ViewModels.Commands;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using AvenueOne.Views.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AvenueOne.ViewModels.WindowsViewModels
{
    public class MainWindowViewModel : WindowViewModel, IMainWindowViewModel, INotifyPropertyChanged
    {
        private PlutoContext _context;

        private Page _currentPage;
        public IUser User { get; set; }
        public Dictionary<string, Page> Pages { get;  private set; }
        public ICommand ChangePageCommand { get; private set; }

        public MainWindowViewModel(Window window, PlutoContext context)
            :base(window)
        {
            if (window == null || context == null)
                throw new ArgumentNullException("Window or context cannot be null.");
            //assign logged in account
            this.User = Settings.Default.UserAccount;
            this._context = context;
            //command
            this.ChangePageCommand = new ChangePageCommand(this);
            //pages
            this.Pages = new Dictionary<string, Page>();
           this.Pages.Add("AdminPage", new AdminPage(_context));
            this.Pages.Add("SettingsPage", new SettingsPage());
            this.Pages.Add("BookingPage", new BookingPage());
            //current page
            this.CurrentPage = Pages["AdminPage"];
        }

        public Page CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}

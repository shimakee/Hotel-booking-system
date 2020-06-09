using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AvenueOne.Views.UserControls
{
    /// <summary>
    /// Interaction logic for UserFormControl.xaml
    /// </summary>
    public partial class UserFormControl : UserControl, INotifyPropertyChanged
    {
        public UserFormControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty UserProperty =
           DependencyProperty.Register("User", typeof(User), typeof(UserFormControl), new PropertyMetadata(null));
        public User User
        {
            get { return (User)GetValue(UserProperty); }
            set
            {
                SetValue(UserProperty, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty IsPasswordIncludedProperty =
           DependencyProperty.Register("IsPasswordIncluded", typeof(bool), typeof(UserFormControl), new PropertyMetadata(null));
        public bool IsPasswordIncluded
        {
            get { return (bool)GetValue(IsPasswordIncludedProperty); }
            set
            {
                SetValue(IsPasswordIncludedProperty, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty UpdateClassCommandProperty =
           DependencyProperty.Register("UpdateClassCommand", typeof(ICommand), typeof(UserFormControl), new PropertyMetadata(null));
        public ICommand UpdateClassCommand
        {
            get { return (ICommand)GetValue(UpdateClassCommandProperty); }
            set
            {
                SetValue(UpdateClassCommandProperty, value);
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}

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
    /// Interaction logic for UserCardControl.xaml
    /// </summary>
    public partial class UserCardControl : UserControl, INotifyPropertyChanged
    {
        public UserCardControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty UserProperty =
           DependencyProperty.Register("User", typeof(User), typeof(UserCardControl), new PropertyMetadata(null));
        public User User
        {
            get { return (User)GetValue(UserProperty); }
            set
            {
                SetValue(UserProperty, value);
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

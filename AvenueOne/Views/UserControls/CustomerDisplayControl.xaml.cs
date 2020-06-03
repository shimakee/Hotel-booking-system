using AvenueOne.Models;
using AvenueOne.Properties;
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
    /// Interaction logic for CustomerDisplayControl.xaml
    /// </summary>
    public partial class CustomerDisplayControl : UserControl, INotifyPropertyChanged
    {

        public CustomerDisplayControl()
        {
            InitializeComponent();
            Settings.Default.UserAccount.PropertyChanged += PropertyChangedEventHandler;
        }

        public static readonly DependencyProperty PersonProperty =
           DependencyProperty.Register("Person", typeof(Customer), typeof(CustomerDisplayControl), new PropertyMetadata(null));
        public Customer Person
        {
            get { return (Customer)GetValue(PersonProperty); }
            set { SetValue(PersonProperty, value);
                OnPropertyChanged();
            }
        }

        public bool IsEditable
        {
            get { return Settings.Default.UserAccount.IsAdmin; }
        }

        //public static readonly DependencyProperty IsEditableProperty =
        //    DependencyProperty.Register("IsEditable", typeof(bool), typeof(CustomerDisplayControl), new PropertyMetadata(Settings.Default.UserAccount.IsAdmin));
        //public bool IsEditable
        //{
        //    get { return (bool)GetValue(IsEditableProperty); }
        //    set { SetValue(IsEditableProperty, value);
        //        OnPropertyChanged();
        //    }
        //}
        private void ChangeVisibility(object sender, RoutedEventArgs e)
        {
            if (IsEditable)
            {
                if (Form.Visibility == Visibility.Visible)
                {
                    Form.Visibility = Visibility.Collapsed;
                    Card.Visibility = Visibility.Visible;
                }
                else
                {
                    Form.Visibility = Visibility.Visible;
                    Card.Visibility = Visibility.Collapsed;
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "IsAdmin")
            {
                OnPropertyChanged("IsEditable");
            }
        }
}
}

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
    /// Interaction logic for CustomerCardControl.xaml
    /// </summary>
    public partial class CustomerCardControl : UserControl, INotifyPropertyChanged
    {
        public CustomerCardControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty CustomerProperty =
           DependencyProperty.Register("Customer", typeof(Customer), typeof(CustomerCardControl), new PropertyMetadata(null));
        public Customer Customer
        {
            get { return (Customer)GetValue(CustomerProperty); }
            set
            {
                SetValue(CustomerProperty, value);
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

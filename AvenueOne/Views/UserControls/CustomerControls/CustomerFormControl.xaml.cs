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
    /// Interaction logic for CustomerFormControl.xaml
    /// </summary>
    public partial class CustomerFormControl : UserControl, INotifyPropertyChanged
    {
        public CustomerFormControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty CustomerProperty =
           DependencyProperty.Register("Customer", typeof(Customer), typeof(CustomerFormControl), new PropertyMetadata(null));
        public Customer Customer
        {
            get { return (Customer)GetValue(CustomerProperty); }
            set
            {
                SetValue(CustomerProperty, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty ClassCommandProperty =
           DependencyProperty.Register("ClassCommand", typeof(ICommand), typeof(CustomerFormControl), new PropertyMetadata(null));
        public ICommand ClassCommand
        {
            get { return (ICommand)GetValue(ClassCommandProperty); }
            set
            {
                SetValue(ClassCommandProperty, value);
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

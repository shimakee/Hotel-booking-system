using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class CustomerDisplayControl : UserControl
    {
        public static readonly DependencyProperty PersonProperty =
           DependencyProperty.Register("Person", typeof(Customer), typeof(CustomerDisplayControl), new PropertyMetadata(null));
        public Customer Person
        {
            get { return (Customer)GetValue(PersonProperty); }
            set { SetValue(PersonProperty, value); }
        }

        public CustomerDisplayControl()
        {
            InitializeComponent();
        }
    }
}

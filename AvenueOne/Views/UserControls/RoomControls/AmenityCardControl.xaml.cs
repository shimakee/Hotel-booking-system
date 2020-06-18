using AvenueOne.Core.Models;
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
    /// Interaction logic for AmenityCardControl.xaml
    /// </summary>
    public partial class AmenityCardControl : UserControl, INotifyPropertyChanged
    {
        public AmenityCardControl()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty AmenityProperty =
           DependencyProperty.Register("Amenity", typeof(Amenities), typeof(AmenityCardControl), new PropertyMetadata(null));
        public Amenities Amenity
        {
            get { return (Amenities)GetValue(AmenityProperty); }
            set
            {
                SetValue(AmenityProperty, value);
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

using AvenueOne.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AmenitiesFormControl.xaml
    /// </summary>
    public partial class AmenitiesFormControl : UserControl, INotifyPropertyChanged
    {
        public AmenitiesFormControl()
        {
            InitializeComponent();
            Name.Focus();
        }



        public static readonly DependencyProperty AmenityProperty =
           DependencyProperty.Register("Amenity", typeof(Amenities), typeof(AmenitiesFormControl), new PropertyMetadata(null));
        public Amenities Amenity
        {
            get { return (Amenities)GetValue(AmenityProperty); }
            set
            {
                SetValue(AmenityProperty, value);
                OnPropertyChanged();
            }
        }

        //public static readonly DependencyProperty RoomTypesProperty =
        //   DependencyProperty.Register("RoomTypes", typeof(ICollection<RoomType>), typeof(AmenitiesFormControl), new PropertyMetadata(null));
        //public ICollection<RoomType> RoomTypes
        //{
        //    get { return (ICollection<RoomType>)GetValue(RoomTypesProperty); }
        //    set
        //    {
        //        SetValue(RoomTypesProperty, value);
        //        OnPropertyChanged();
        //    }
        //}

        public static readonly DependencyProperty ClassCommandProperty =
           DependencyProperty.Register("ClassCommand", typeof(ICommand), typeof(AmenitiesFormControl), new PropertyMetadata(null));
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

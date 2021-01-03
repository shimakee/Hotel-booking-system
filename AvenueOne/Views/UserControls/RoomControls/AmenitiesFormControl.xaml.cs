using AvenueOne.Core.Models;
using AvenueOne.ViewModels.TabViewModels;
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

        //public static readonly DependencyProperty RoomTypeSelectedProperty =
        //    DependencyProperty.Register("RoomTypeSelected", typeof(RoomType), typeof(AmenitiesFormControl), new PropertyMetadata(null));
        //public RoomType RoomTypeSelected
        //{
        //    get { return (RoomType)GetValue(RoomTypeSelectedProperty); }
        //    set
        //    {
        //        SetValue(RoomTypeSelectedProperty, value);
        //        OnPropertyChanged();
        //    }
        //}

        public static readonly DependencyProperty RoomTypeViewModelProperty =
            DependencyProperty.Register("RoomTypeViewModel", typeof(RoomTypeViewModel), typeof(AmenitiesFormControl), new PropertyMetadata(null));
        public RoomTypeViewModel RoomTypeViewModel
        {
            get { return (RoomTypeViewModel)GetValue(RoomTypeViewModelProperty); }
            set
            {
                SetValue(RoomTypeViewModelProperty, value);
                OnPropertyChanged();
            }
        }

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

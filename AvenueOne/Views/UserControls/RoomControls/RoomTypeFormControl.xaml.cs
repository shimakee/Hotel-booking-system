using AvenueOne.Core.Models;
using AvenueOne.ViewModels;
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
    /// Interaction logic for RoomTypeFormControl.xaml
    /// </summary>
    public partial class RoomTypeFormControl : UserControl
    {
        public RoomTypeFormControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty AmenitiesCollectionProperty =
          DependencyProperty.Register("AmenitiesCollection", typeof(ObservableCollection<Amenities>), typeof(RoomTypeFormControl), new PropertyMetadata(null));
        public ObservableCollection<Amenities> AmenitiesCollection
        {
            get { return (ObservableCollection<Amenities>)GetValue(AmenitiesCollectionProperty); }
            set
            {
                SetValue(AmenitiesCollectionProperty, value);
                OnPropertyChanged();
            }
        }

        //public static readonly DependencyProperty AmenitySelectedProperty =
        //  DependencyProperty.Register("AmenitySelected", typeof(Amenities), typeof(RoomTypeFormControl), new PropertyMetadata(null));
        //public Amenities AmenitySelected
        //{
        //    get { return (Amenities)GetValue(AmenitySelectedProperty); }
        //    set
        //    {
        //        SetValue(AmenitySelectedProperty, value);
        //        OnPropertyChanged();
        //    }
        //}

        public static readonly DependencyProperty RoomTypeViewModelProperty =
          DependencyProperty.Register("RoomTypeViewModel", typeof(RoomTypeViewModel), typeof(RoomTypeFormControl), new PropertyMetadata(null));
        public RoomTypeViewModel RoomTypeViewModel
        {
            get { return (RoomTypeViewModel) GetValue(RoomTypeViewModelProperty); }
            set
            {
                SetValue(RoomTypeViewModelProperty, value);
                OnPropertyChanged();
            }
        }

        //public RoomType RoomType
        //{
        //    get { return RoomTypeViewModel.ModelSelected; }
        //    set { RoomTypeViewModel.ModelSelected = value; }
        //}

        //public static readonly DependencyProperty RoomTypeProperty =
        //  DependencyProperty.Register("RoomType", typeof(RoomType), typeof(RoomTypeFormControl), new PropertyMetadata(null));
        //public RoomType RoomType
        //{
        //    get { return (RoomType)GetValue(RoomTypeProperty); }
        //    set
        //    {
        //        SetValue(RoomTypeProperty, value);
        //        OnPropertyChanged();
        //    }
        //}

        public static readonly DependencyProperty LinkCommandProperty =
          DependencyProperty.Register("LinkCommand", typeof(ICommand), typeof(RoomTypeFormControl), new PropertyMetadata(null));
        public ICommand LinkCommand
        {
            get { return (ICommand)GetValue(LinkCommandProperty); }
            set
            {
                SetValue(LinkCommandProperty, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty DetachCommandProperty =
          DependencyProperty.Register("DetachCommand", typeof(ICommand), typeof(RoomTypeFormControl), new PropertyMetadata(null));
        public ICommand DetachCommand
        {
            get { return (ICommand)GetValue(DetachCommandProperty); }
            set
            {
                SetValue(DetachCommandProperty, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty ClassCommandProperty =
          DependencyProperty.Register("ClassCommand", typeof(ICommand), typeof(RoomTypeFormControl), new PropertyMetadata(null));
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

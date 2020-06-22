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
    /// Interaction logic for RoomTypeFormControl.xaml
    /// </summary>
    public partial class RoomTypeFormControl : UserControl
    {
        public RoomTypeFormControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty RoomTypeProperty =
          DependencyProperty.Register("RoomType", typeof(RoomType), typeof(RoomTypeCardControl), new PropertyMetadata(null));
        public RoomType RoomType
        {
            get { return (RoomType)GetValue(RoomTypeProperty); }
            set
            {
                SetValue(RoomTypeProperty, value);
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

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
    /// Interaction logic for TextBoxControl.xaml
    /// </summary>
    public partial class TextBoxControl : UserControl, INotifyPropertyChanged
    {

        public static readonly DependencyProperty ImageSourceProperty =
           DependencyProperty.Register("ImageSource", typeof(BitmapSource), typeof(TextBoxControl), new PropertyMetadata(null));
        public BitmapSource ImageSource
        {
            get { return (BitmapSource)GetValue(ImageSourceProperty); }
            set
            {
                SetValue(ImageSourceProperty, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty LabelProperty =
               DependencyProperty.Register("Label", typeof(string), typeof(TextBoxControl), new PropertyMetadata(null));
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty TextProperty =
               DependencyProperty.Register("Text", typeof(string), typeof(TextBoxControl), new PropertyMetadata(null));
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value);
                OnPropertyChanged();
            }
        }


        public TextBoxControl()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        public void OnPropertyChanged([CallerMemberName] String property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}

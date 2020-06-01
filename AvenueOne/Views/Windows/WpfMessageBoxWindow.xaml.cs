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
using System.Windows.Shapes;

namespace AvenueOne.Views.Windows
{
    /// <summary>
    /// Interaction logic for WpfMessageBoxWindow.xaml
    /// </summary>
    public partial class WpfMessageBoxWindow : Window
    {
        public WpfMessageBoxWindow()
        {
            InitializeComponent();
        }

        //public WpfMessageBoxWindow(string message)
        //    :this()
        //{
          
        //}

        private void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 1)
            {
                //    if (this.WindowState == WindowState.Maximized)
                //    {
                //        this.WindowState = WindowState.Normal;
                //    }
                //    else
                //    {
                //        this.WindowState = WindowState.Maximized;
                //    }
                //}
                //else
                //{

                this.DragMove();

            }
        }
        private void MinimizeWindow(object sender, RoutedEventArgs e) //no need to complicate and implement an ICommand?
        {
            this.WindowState = WindowState.Minimized;
        }
        private void MaximizeWindow(object sender, RoutedEventArgs e)
        {
            //if (this.WindowState == WindowState.Maximized)
            //{
            this.WindowState = WindowState.Normal;
            //}
            //else
            //{
            //    this.WindowState = WindowState.Maximized;
            //}
        }
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #region Override
        private void ReplaceTextblocks(string message, string caption)
        {
            if (message == null)
                message = "";

            TextblockContent.Text = message;
            TextCaption.Text = caption;
        }

        public void MessageDialog(string message)
        {
            this.MessageDialog(message, "");
        }

        public void MessageDialog(string message, string caption)
        {
            this.ReplaceTextblocks(message, caption);
            this.ShowDialog();
        }

        public void Message(string message)
        {
            this.Message(message, "");
        }
        public void Message(string message, string caption)
        {
            this.ReplaceTextblocks(message, caption);
            this.Show();
        }
        #endregion
    }
}

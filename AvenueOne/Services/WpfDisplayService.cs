using AvenueOne.Services.Interfaces;
using AvenueOne.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AvenueOne.Services
{
    public class WpfDisplayService : IDisplayService
    {
        private WpfMessageBoxWindow _wpfMessageWindow { get; set; }

        public WpfDisplayService()
        {
        }

        public Window CreateMainWindow() // this should not be here. - where to put it?
        {
            return new MainWindow();
        }

        public void MessageDisplay(string message)
        {
            //MessageBox.Show(message);
            this.MessageDisplay(message, "");
        }

        public void MessageDisplay(string message, string caption)
        {
            //MessageBox.Show(message, caption);
            GenerateMessageBox();
            this._wpfMessageWindow.MessageDialog(message, caption);

        }

        public void ErrorDisplay(string message, string caption)
        {
            //MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Warning);
            GenerateMessageBox();
            this._wpfMessageWindow.MessageDialog(message, caption);

        }

        public bool MessagePrompt(string message, string caption)
        {
            MessageBoxResult result = MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (result == MessageBoxResult.Yes)
                return true;
            return false;
        }

        public bool? MessagePromptNullable(string message, string caption)
        {
            MessageBoxResult result = MessageBox.Show(message, caption, MessageBoxButton.YesNoCancel, MessageBoxImage.Information);

            if (result == MessageBoxResult.Yes)
                return true;
            if (result == MessageBoxResult.No)
                return false;
            return null;
        }

        private void GenerateMessageBox()
        {
                this._wpfMessageWindow = new WpfMessageBoxWindow();
        }

    }
}

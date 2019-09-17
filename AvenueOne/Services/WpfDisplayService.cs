using AvenueOne.Services.Interfaces;
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
        public Window CreateMainWindow()
        {
            return new MainWindow();
        }

        public void MessageDisplay(string message)
        {
            MessageBox.Show(message);
        }

        public void MessageDisplay(string message, string caption)
        {
            MessageBox.Show(message, caption);
        }
        
        public void ErrorDisplay(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Warning);
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
    }
}

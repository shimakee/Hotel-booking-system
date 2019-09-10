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

        public void DisplayMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}

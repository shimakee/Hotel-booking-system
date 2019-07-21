using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AvenueOne
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void OnStartup( object sender, StartupEventArgs args)
        {
            EventManager.RegisterClassHandler(typeof(TextBox), TextBox.KeyDownEvent, new KeyEventHandler(TextBox_KeyDown));
            EventManager.RegisterClassHandler(typeof(CheckBox), CheckBox.KeyDownEvent, new KeyEventHandler(CheckBox_KeyDown));
        }

        private void CheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            MoveToNextUIelement(e);

            if(e.Handled == true)
            {
                CheckBox checkbox = (CheckBox)sender;
                checkbox.IsChecked = !checkbox.IsChecked;
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter & (sender as TextBox).AcceptsReturn == false)
                MoveToNextUIelement(e);
        }

        private void MoveToNextUIelement(KeyEventArgs e)
        {
            //Get keyboard focus element
            UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;

            if (elementWithFocus != null)
            {
                // Make ChangeFocuse request with change direction as arguement
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                //move fovus from focused element  to different ui element based on request
                if (elementWithFocus.MoveFocus(request))
                    e.Handled = true;
            }
        }
    }
}

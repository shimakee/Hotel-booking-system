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
            //event handler for easy navigation
            EventManager.RegisterClassHandler(typeof(TextBox), TextBox.KeyDownEvent, new KeyEventHandler(TextBox_KeyDown));
            EventManager.RegisterClassHandler(typeof(CheckBox), CheckBox.KeyDownEvent, new KeyEventHandler(CheckBox_KeyDown));
            //event handler for hotkeys
            EventManager.RegisterClassHandler(typeof(Window), Window.KeyDownEvent, new KeyEventHandler(Window_KeyDown));
        }

        //hotkeys
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                MessageBox.Show("it works");
                e.Handled = true;
            }
        }

        private void CheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                MoveToNextUIelement(e);
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.OemTilde)
                MoveToPreviousUIelement(e);

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

        private void MoveToPreviousUIelement(KeyEventArgs e)
        {
            UIElement focusedElement = Keyboard.FocusedElement as UIElement;

            if(focusedElement != null)
            {
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Previous);
                if (focusedElement.MoveFocus(request))
                    e.Handled = true;

            }
        }

    }
}

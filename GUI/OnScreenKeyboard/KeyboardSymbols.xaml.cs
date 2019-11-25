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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FinalProject1.GUI.OnScreenKeyboard
{
    /// <summary>
    /// Interaction logic for KeyboardSymbols.xaml
    /// </summary>
    public partial class KeyboardSymbols : Page
    {

        OnScreenKeyboard owner;
        Frame keyboardFrame;
        Control editField;

        public KeyboardSymbols(OnScreenKeyboard inKeyboard, Frame inFrame, Control inEditField)
        {
            InitializeComponent();

            owner = inKeyboard;
            keyboardFrame = inFrame;
            editField = inEditField;
        }

        /// <summary>
        /// Update keyboard size based on window size
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Height = ActualWidth * .25;
        }

        /// <summary>
        /// Swich the page and go back to the Alpha-Numeric Keyboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ABCKeyPress(object sender, TouchEventArgs e)
        {
            keyboardFrame.GoBack();
        }

        /// <summary>
        /// Update the text field with the symbol associated with the label pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void InputKeyPress(object sender, TouchEventArgs e)
        {
            if (editField is TextBox)
            {
                ((TextBox)editField).Text += ((Label)sender).Content.ToString();
            }
            else
            {
                ((PasswordBox)editField).Password += ((Label)sender).Content.ToString();
            }
            ((Label)sender).Background = Brushes.Gray;
        }

        /// <summary>
        /// Update the label color when touch enters bounds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputKeyDown(object sender, TouchEventArgs e)
        {
            ((Label)sender).Background = Brushes.CadetBlue;
        }

        /// <summary>
        /// close the window and return to underlying screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void EnterKeyPressed(object sender, TouchEventArgs e)
        {
            owner.Finish();
        }

        /// <summary>
        /// update the label color when touch leaves bounds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
      public void TouchLeftKey(object sender, TouchEventArgs e)
        {
            ((Label)sender).Background = Brushes.Gray;
        }

        /// <summary>
        /// remove the last character from the text field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BackspaceKeyPressed(object sender, TouchEventArgs e)
        {
            if (editField is TextBox)
            {
                string text = ((TextBox)editField).Text.ToString();
                if (text.Length > 0)
                {
                    ((TextBox)editField).Text = text.Substring(0, text.Length - 1);
                }
            }
            else
            {
                string text = ((PasswordBox)editField).Password.ToString();
                if (text.Length > 0)
                {
                    ((PasswordBox)editField).Password = text.Substring(0, text.Length - 1);
                }
            }
            ((Label)sender).Background = Brushes.Gray;
        }
    }
}

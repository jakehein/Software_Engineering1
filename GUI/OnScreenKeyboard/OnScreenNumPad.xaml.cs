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

namespace FinalProject1.GUI.OnScreenKeyboard
{
    /// <summary>
    /// On screen number pad for touch input
    /// </summary>
    public partial class OnScreenNumPad : Window
    {
        private const string EnterText = "E\nN\nT\nE\nR";
        
        string result = "";
        public OnScreenNumPad(string startingText)
        {
            InitializeComponent();
            keyboardValue.Content = startingText;
            buttonEnter.Content = EnterText;
        }

        /// <summary>
        /// Remove the last digit from the current value
        /// </summary>
        /// <param name="sender">The object that caused the call</param>
        /// <param name="e">Specific event data</param>
        private void BackspacePressed(object sender, TouchEventArgs e)
        {
            if (!keyboardValue.Content.ToString().Equals(""))
            {
                keyboardValue.Content = keyboardValue.Content.ToString().Substring(0, keyboardValue.Content.ToString().Length - 1);
            }
        }

        /// <summary>
        /// Get the stored result
        /// </summary>
        /// <returns>The value entered</returns>
        public string GetResult()
        {
            return result;
        }

        /// <summary>
        /// Finish entering the data
        /// </summary>
        /// <param name="sender"> The object that caused the call</param>
        /// <param name="e">Specific event data</param>
        private void EnterPressed(object sender, TouchEventArgs e)
        {
            result = keyboardValue.Content.ToString();
            DialogResult = true;
            Close();
        }

        /// <summary>
        /// Append the pressed number to the current value
        /// </summary>
        /// <param name="sender"> The object that caused the call</param>
        /// <param name="e">Specific event data</param>
        private void NumberButtonPressed(object sender, TouchEventArgs e)
        {
            keyboardValue.Content += ((Label)sender).Content.ToString();
        }

        /// <summary>
        /// Change the appearance when touch enters
        /// </summary>
        /// <param name="sender"> The object that caused the call</param>
        /// <param name="e">Specific event data</param>
        private void KeyTouchEntered(object sender, TouchEventArgs e)
        {
            ((Label)sender).Background = Brushes.CadetBlue;
        }

        /// <summary>
        /// Change the appearance when touch leaves
        /// </summary>
        /// <param name="sender"> The object that caused the call</param>
        /// <param name="e">Specific event data</param>
        private void KeyTouchLeave(object sender, TouchEventArgs e)
        {
            if (((Label)sender).Content.Equals("Enter"))
            {
                ((Label)sender).Background = Brushes.Linen;
            }
            else if (((Label)sender).Content.Equals("<-"))
            {
                ((Label)sender).Background = Brushes.Transparent;
            }
            else
            {
                ((Label)sender).Background = Brushes.Transparent;
            }
        }

        /// <summary>
        /// Resize Size of number and backspace text
        /// </summary>
        /// <param name="sender"> The object that caused the call</param>
        /// <param name="e">Specific event data</param>
        private void KeyboardValue_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((Label)sender).FontSize = ((Label)sender).ActualHeight * .7;
        }

        /// <summary>
        /// Resize enter button text
        /// </summary>
        /// <param name="sender"> The object that caused the call</param>
        /// <param name="e">Specific event data</param>
        private void ButtonEnter_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((Label)sender).FontSize = ((Label)sender).ActualHeight / 7;
        }
    }
}

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
    /// Interaction logic for Keyboard.xaml
    /// </summary>
    public partial class OnScreenKeyboard : Window
    {
        public enum InputType { TEXT, PASSWORD }

        Control currentInputField;
        string result = "";

        public OnScreenKeyboard(InputType type, Window owner, String previousText)
        {
            InitializeComponent();
            Owner = owner;
            if (type == InputType.TEXT)
            {
                txtTempInput.Visibility = Visibility.Visible;
                passTempInput.Visibility = Visibility.Hidden;
                currentInputField = txtTempInput;
                txtTempInput.Text = previousText;
            }
            else
            {
                txtTempInput.Visibility = Visibility.Hidden;
                passTempInput.Visibility = Visibility.Visible;
                currentInputField = passTempInput;
                passTempInput.Password = previousText;
            }

            keyboardFrame.Navigate(new KeyboardABC(this, keyboardFrame, currentInputField));
        }

        /// <summary>
        /// store the textboxvalue and close the window
        /// </summary>
        public void Finish()
        {
            result = currentInputField is TextBox ? txtTempInput.Text : passTempInput.Password;
            Close();
        }
        
        /// <summary>
        /// get the result entered in the textbox before close
        /// </summary>
        /// <returns></returns>
        public string GetResult()
        {
            return result;
        }

        /// <summary>
        /// resize the window to relative size of containing window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double ownerWidth = Owner.ActualWidth;
            double ownerHeight = Owner.ActualHeight;
            Width = ownerWidth * .6;
            Height = ActualWidth * .4;
            Left = ownerWidth * .5 - ActualWidth * .5;
            Top = ownerHeight - ActualHeight - 30;
        }

        /// <summary>
        /// resize the textBox font to relative control size
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtTempInput_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((TextBox)sender).FontSize = ((TextBox)sender).ActualHeight * .7;
        }

        /// <summary>
        /// resize the passwordBox font to relative control size
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PassTempInput_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((PasswordBox)sender).FontSize = ((PasswordBox)sender).ActualHeight * .7;
        }
    }
}

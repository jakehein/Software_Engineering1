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
    /// Interaction logic for KeyboardABC.xaml
    /// </summary>
    public partial class KeyboardABC : Page
    {
        private const long CapsLockKeyPressTime = 500;

        OnScreenKeyboard owner;
        Frame keyboardFrame;
        Control editField;
        bool isUpperCase = false;
        bool isCapsLock = false;
        long shiftLastPressedTime;
        List<Button> letterButtons = new List<Button>();


        public KeyboardABC(OnScreenKeyboard inKeyboard, Frame inFrame, Control inEditField)
        {
            InitializeComponent();
            keyboardFrame = inFrame;
            editField = inEditField;
            owner = inKeyboard;
            AddLetterButtonsToList();
        }

        /// <summary>
        /// Add all of the letter buttons to an array for easy Upper/Lower case conversion
        /// </summary>
        private void AddLetterButtonsToList()
        {
            letterButtons.Add(btnA);
            letterButtons.Add(btnB);
            letterButtons.Add(btnC);
            letterButtons.Add(btnD);
            letterButtons.Add(btnE);
            letterButtons.Add(btnF);
            letterButtons.Add(btnG);
            letterButtons.Add(btnH);
            letterButtons.Add(btnI);
            letterButtons.Add(btnJ);
            letterButtons.Add(btnK);
            letterButtons.Add(btnL);
            letterButtons.Add(btnM);
            letterButtons.Add(btnN);
            letterButtons.Add(btnO);
            letterButtons.Add(btnP);
            letterButtons.Add(btnQ);
            letterButtons.Add(btnR);
            letterButtons.Add(btnS);
            letterButtons.Add(btnT);
            letterButtons.Add(btnU);
            letterButtons.Add(btnV);
            letterButtons.Add(btnW);
            letterButtons.Add(btnX);
            letterButtons.Add(btnY);
            letterButtons.Add(btnZ);
        }

        /// <summary>
        /// Swap to the symbol key page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_TouchUp(object sender, TouchEventArgs e)
        {
           //keyboardFrame.Navigate(new KeyboardNum(keyboardFrame));

        }

        /// <summary>
        /// Resize the keyboard on window resize
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Height = ActualWidth * .25;
        }

        /// <summary>
        /// Parse the value of the button and append it to the textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputKeyPress(object sender, TouchEventArgs e)
        {
            if (editField is TextBox)
            {
                ((TextBox)editField).Text += ((Button)sender).Content.ToString();
            }
            else
            {
                ((PasswordBox)editField).Password += ((Button)sender).Content.ToString();
            }
            if(isUpperCase && !isCapsLock)
            {
                SwapKeyCase();
            }
        }

        /// <summary>
        /// Close the form 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterKeyPressed(object sender, TouchEventArgs e)
        {
            owner.Finish();
        }

        /// <summary>
        /// Shift from lowercase to upper and upper to lower
        /// </summary>
        private void SwapKeyCase()
        {        
            BrushConverter bc = new BrushConverter();
            int offset;
            if (isUpperCase)
            {
                offset = 32;
                btnLeftShift.Background = (Brush)bc.ConvertFrom("#6666FF");
                btnRightShift.Background = (Brush)bc.ConvertFrom("#6666FF");
            }
            else
            {
                offset = -32;
                btnLeftShift.Background = (Brush)bc.ConvertFrom("#666666");
                btnRightShift.Background = (Brush)bc.ConvertFrom("#666666");
            }
            foreach (Button btn in letterButtons)
            {
                btn.Content = (char)(btn.Content.ToString()[0] + offset);
            }
            isUpperCase = !isUpperCase;
        }

        /// <summary>
        /// Test for capslock toggle and shift case
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShiftKeyPressed(object sender, TouchEventArgs e)
        {
            long currentShiftTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            if (isCapsLock)
            {
                isCapsLock = false;
                SwapKeyCase();
            }
            else if (currentShiftTime - shiftLastPressedTime < CapsLockKeyPressTime)
            {
                isCapsLock = true;
            }
            else
            {
                SwapKeyCase();
            }
            shiftLastPressedTime = currentShiftTime;
        }

        /// <summary>
        /// remove the last character from the textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackspacePressed(object sender, TouchEventArgs e)
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
            if (isUpperCase && !isCapsLock)
            {
                SwapKeyCase();
            }
        }
    }
}

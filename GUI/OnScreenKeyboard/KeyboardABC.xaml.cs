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
        private const long CapsLockKeyPressTime = 400;

        OnScreenKeyboard owner;
        Frame keyboardFrame;
        Control editField;
        bool isUpperCase = false;
        bool isCapsLock = false;
        long shiftLastPressedTime;
        List<Label> letterButtons = new List<Label>();


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
        private void SymbolsKeyPress(object sender, TouchEventArgs e)
        {
            ((Label)sender).Background = Brushes.Gray;
           keyboardFrame.Navigate(new KeyboardSymbols(owner, keyboardFrame, editField));
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
                ((TextBox)editField).Text += ((Label)sender).Content.ToString();
            }
            else
            {
                ((PasswordBox)editField).Password += ((Label)sender).Content.ToString();
            }
            if(isUpperCase && !isCapsLock)
            {
                SwapKeyCase();
                btnLeftShift.Background = Brushes.Gray;
                btnRightShift.Background = Brushes.Gray;
            }
        }
        
        /// <summary>
        /// Update label color when touch enters bounds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputKeyDown(object sender, TouchEventArgs e)
        {
            ((Label)sender).Background = Brushes.CadetBlue;
            
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
            int offset;
            if (isUpperCase)
            {
                offset = 32;
            }
            else
            {
                offset = -32;                
            }
            foreach (Label btn in letterButtons)
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
                shiftLastPressedTime = 0;
                SwapKeyCase();
            }
            else if (currentShiftTime - shiftLastPressedTime < CapsLockKeyPressTime)
            {
                isCapsLock = true;
                shiftLastPressedTime = currentShiftTime;
                if (!isUpperCase)
                {
                    SwapKeyCase();
                }
            }
            else
            {
                SwapKeyCase();
                shiftLastPressedTime = currentShiftTime;
            }
            
        }

        /// <summary>
        /// Update label colors when touch leaves label bounds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TouchLeftKey(object sender, TouchEventArgs e)
        {
            if (sender == btnLeftShift || sender == btnRightShift)
            {

                BrushConverter bc = new BrushConverter();
                if (isCapsLock)
                {
                    btnLeftShift.Background = Brushes.Blue;
                    btnRightShift.Background = Brushes.Blue;
                }
                else if(isUpperCase)
                {
                    btnLeftShift.Background = Brushes.LightBlue;
                    btnRightShift.Background = Brushes.LightBlue;
                }
                else
                {
                    btnLeftShift.Background = Brushes.Gray;
                    btnRightShift.Background = Brushes.Gray;
                }                
            }
            else
            {
                ((Label)sender).Background = Brushes.LightGray;
            }
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
            ((Label)sender).Background = Brushes.Gray;
        }
    }
}

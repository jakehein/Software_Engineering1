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

namespace FinalProject1
{
    /// <summary>
    /// Interaction logic for CashTillStartPage.xaml
    /// </summary>
    public partial class CashTillStartPage : Page
    {
        private IDrawerController drawerController => ControllerContainer.Instance.DrawerController;

        public CashTillStartPage()
        {
            InitializeComponent();
            drawerController.ResetDrawer();
            CashAmountText.Focus();
        }
        
        /// <summary>
        /// Takes the entered amount and sets up the drawer to that amount and then navigates to the MainMenu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            decimal cashDecimal;
            string cashAmount = CashAmountText.Text;
            bool isDecimal = decimal.TryParse(cashAmount, out cashDecimal);

            if (IsValidAmount())
            {
                drawerController.AddToDrawer(cashDecimal);
                this.NavigationService.Navigate(new MainMenu());
            }
            else
            {
                MessageBox.Show("Not a valid starting amount. Please Try Again.");
                CashAmountText.Text = "";
            }
        }

        /// <summary>
        /// Checks that the entered amount is a valid input.
        /// </summary>
        /// <returns>True if the amount within CashAmountText confroms to valid decimal format</returns>
        public bool IsValidAmount()
        {
            decimal cashDecimal;
            string cashAmount = CashAmountText.Text;
            bool isDecimal = decimal.TryParse(cashAmount, out cashDecimal);

            if (isDecimal && (cashDecimal >= 0) && (cashDecimal * 100 == Math.Floor(cashDecimal * 100)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CashAmountText_TouchUp(object sender, TouchEventArgs e)
        {
            GUI.OnScreenKeyboard.OnScreenNumPad numberPad = new GUI.OnScreenKeyboard.OnScreenNumPad(CashAmountText.Text);
            bool? dialogResult = numberPad.ShowDialog();
            if(dialogResult != null && (bool)dialogResult)
            {
                CashAmountText.Text = numberPad.GetResult();
            }
        }

        /// <summary>
        /// Update the font size on window resize
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CashAmountText_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((TextBox)sender).FontSize = ((TextBox)sender).ActualHeight * .7;
        }
    }
}

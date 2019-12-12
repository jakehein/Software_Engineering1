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
    /// Interaction logic for ChangeKeyboard.xaml
    /// </summary>
    public partial class ChangeKeyboard : Window
    {
        private IDrawerController drawerController => ControllerContainer.Instance.DrawerController;

        public ChangeKeyboard(string transactionTotal)
        {
            InitializeComponent();
            EnterBtn.Content = EnterText;
            cashTotal = transactionTotal;
            EnterBtn.IsEnabled = false;
            IsTransactionComplete = false;
        }

        /// <summary>
        /// Total amount that the transaction is worth.
        /// </summary>
        private string cashTotal;

        /// <summary>
        /// Gets or sets the amount of money given to the user when making a purchase.
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// Returns True or False depending on whether the user has completed the transaction.
        /// </summary>
        public bool IsTransactionComplete { get; set; }

        /// <summary>
        /// Displays "Enter" vertically on the EnterBtn.
        /// </summary>
        private const string EnterText = "E\nN\nT\nE\nR";

        /// <summary>
        /// Takes in a string value and concatenates it to the end of TotalLabel's contents.
        /// The decimal place is moved accordingly, and a check to IsEnterEnabled is called.
        /// </summary>
        /// <param name="value">string character to be inserted</param>
        private void Increase_PayTotal(string value)
        {
            string total = (string)TotalLabel.Content;
            total = total.Remove(total.IndexOf('.'), 1);
            if (total[0] == '0')
            {
                total = total.Substring(1);
            }
            total += value;
            TotalLabel.Content = total.Insert(total.Length - 2, ".");

            IsEnterEnabled();

        }

        /// <summary>
        /// Compares the contents of TotalLabel and cashTotal and enables
        /// the EnterBtn if TotalLabel >= cashTotal, else it is disabled.
        /// </summary>
        private void IsEnterEnabled()
        {
            Double a = Double.Parse((string)TotalLabel.Content);
            Double b = Double.Parse(cashTotal);

            if (b <= a)
            {
                EnterBtn.IsEnabled = true;
            }
            else
            {
                EnterBtn.IsEnabled = false;
            }
        }

        /// <summary>
        /// The character at the end of TotalLabel's contents is removed and the
        /// decimal place is moved accordingly. A check to IsEnterEnabled is then called.
        /// </summary>
        private void Decrease_PayTotal()
        {
            string total = (string)TotalLabel.Content;
            total = total.Remove(total.IndexOf('.'), 1);
            total = total.Remove(total.Length - 1);
            if (total.Length < 3)
            {
                total = total.Insert(0, "0");
            }
            TotalLabel.Content = total.Insert(total.Length - 2, ".");

            IsEnterEnabled();
        }

        /// <summary>
        /// Changes the font size of the keyboard numbers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Number_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((Label)sender).FontSize = ((Label)sender).ActualHeight / 2;
        }

        /// <summary>
        /// Changes the font size of the keyboard EnterBtn.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Enter_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((Label)sender).FontSize = ((Label)sender).ActualWidth / 5;
            ((Label)sender).FontSize = ((Label)sender).ActualHeight / 7;
        }

        /// <summary>
        /// Changes the font size of the keyboard TotalLabel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Total_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((Label)sender).FontSize = ((Label)sender).ActualHeight / 1.5;
        }

        /// <summary>
        /// Calls the Decrease_PayTotal method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Decrease_PayTotal();
        }

        /// <summary>
        /// Calls the Increase_PayTotal method by passing in the sender's string value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Number_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Increase_PayTotal(((Label)sender).Content.ToString());
        }

        /// <summary>
        /// Closes the ChangeKeyboard window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // After cash is taken out this will determine if the cash left in the drawer is low or high
            LowCashWarningCheck();
            HighCashWarningCheck();
            Close();
        }

        /// <summary>
        /// Sets Total to the TotalLabel's contents, sets IsTransactionComplete to true,
        /// and then closes the ChangeKeyboard window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Total = decimal.Parse((String)TotalLabel.Content);
            IsTransactionComplete = true;
            CancelBtn_MouseUp(sender, e);
        }

        /// <summary>
        /// This method checks the amount of money present in the till and displays a warning to the user if it is determined
        /// as low.
        /// </summary>
        public void LowCashWarningCheck()
        {
            if (drawerController.cashIsLow())
            {
                decimal amountInDrawer = drawerController.CurrentCashInDrawer();
                MessageBox.Show("Cash Till is Running Low. Amount is currently: $" + amountInDrawer + ". Please Add More.");
            }
        }

        /// <summary>
        /// This method checks the amount of money present in the till and displays a warning to the user if it is determined
        /// as high.
        /// </summary>
        public void HighCashWarningCheck()
        {
            if (drawerController.cashIsHigh())
            {
                decimal amountInDrawer = drawerController.CurrentCashInDrawer();
                MessageBox.Show("Cash Till is Running High. Amount is currently: $" + amountInDrawer + ". Please Withdraw More.");
            }
        }
    }
}

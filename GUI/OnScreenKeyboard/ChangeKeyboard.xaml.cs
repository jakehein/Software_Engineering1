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

        public string cashTotal;
        public decimal Total { get; set; }
        public bool IsTransactionComplete { get; set; }

        private const string EnterText = "E\nN\nT\nE\nR";
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

        private void Number_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((Label)sender).FontSize = ((Label)sender).ActualHeight / 2;
        }

        private void Enter_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((Label)sender).FontSize = ((Label)sender).ActualWidth / 5;
            ((Label)sender).FontSize = ((Label)sender).ActualHeight / 7;
        }

        private void Total_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((Label)sender).FontSize = ((Label)sender).ActualHeight / 1.5;
        }

        private void DeleteBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Decrease_PayTotal();
        }

        private void Number_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Increase_PayTotal(((Label)sender).Content.ToString());
        }

        private void CancelBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // After cash is taken out this will determine if the cash left in the drawer is low or high
            LowCashWarningCheck();
            HighCashWarningCheck();
            Close();
        }

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

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
    /// Interaction logic for CashTillPage.xaml
    /// </summary>
    public partial class CashTillPage : Page
    {
        private IDrawerController drawerController;

        public CashTillPage()
        {
            InitializeComponent();
            drawerController = new DrawerController();
            UpdateTillDisplay();
        }

        /// <summary>
        /// Updates the Amount displayed so that it is equal to the amount within the drawer.
        /// </summary>
        public void UpdateTillDisplay()
        {
            decimal currentAmount = drawerController.CurrentCashInDrawer();
            CashLabel.Text = "$" + currentAmount;
            CashAmountText.Text = "";
        }

        /// <summary>
        /// Adds amount specified within CashAmountText to the till/drawer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            decimal cashDecimal;
            string cashAmount = CashAmountText.Text;
            bool isDecimal = decimal.TryParse(cashAmount, out cashDecimal);

            if (IsValidAmount())
            {
                decimal amountAdded = drawerController.AddToDrawer(cashDecimal);
                if (amountAdded >= 0)
                {
                    MessageBox.Show("$" + cashDecimal + " has been successfully added.");
                    UpdateTillDisplay();
                }
                else
                {
                    MessageBox.Show("Add was not successful. Please Try Again.");
                }
            }
            else
            {
                MessageBox.Show("Not a Valid Amount. Please Try Again.");
                CashAmountText.Text = "";
            }
        }

        /// <summary>
        /// Withdrawls amount specified within CashAmountText from the till/drawer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WithdrawlBtn_Click(object sender, RoutedEventArgs e)
        {
            decimal cashDecimal;
            string cashAmount = CashAmountText.Text;
            bool isDecimal = decimal.TryParse(cashAmount, out cashDecimal);

            if (IsValidAmount())
            {
                decimal amountTaken = drawerController.WithdrawlFromDrawer(cashDecimal);
                if (amountTaken >= 0)
                {
                    MessageBox.Show("$" + cashDecimal + " has been successfully withdrawn.");
                    UpdateTillDisplay();
                }
                else
                {
                    MessageBox.Show("Withdrawl was not successful. Please Try Again.");
                } 
            }
            else
            {
                MessageBox.Show("Not a Valid Amount. Please Try Again.");
                CashAmountText.Text = "";
            }
        }

        /// <summary>
        /// Directs the user back to the Main Menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        /// <summary>
        /// Checks that the entered amount is a valid input.
        /// </summary>
        /// <returns>true if the amount is in a valid decimal format</returns>
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
    }
}

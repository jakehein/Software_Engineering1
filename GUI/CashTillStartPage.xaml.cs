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
        public CashTillStartPage()
        {
            InitializeComponent();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            decimal cashDecimal;
            string cashAmount = CashAmountText.Text;
            bool isDecimal = decimal.TryParse(cashAmount, out cashDecimal);

            if (isDecimal)
            {
                Drawer drawer = new Drawer();
                drawer.Money = cashDecimal;
                //MessageBox.Show(cashDecimal + "");
                this.NavigationService.Navigate(new MainMenu());
            }
            else
            {
                MessageBox.Show("Not a valid starting Amount. Please Try Again.");
                CashAmountText.Text = "";
            }
        }
    }
}

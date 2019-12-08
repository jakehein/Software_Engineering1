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
        public ChangeKeyboard(string transactionTotal)
        {
            InitializeComponent();
            EnterBtn.Content = EnterText;
            cashTotal = transactionTotal;
            EnterBtn.IsEnabled = false;
            IsTransactionComplete = false;
        }
        //private string cashTotal;
        //private bool isTransactionComplete;
        public string cashTotal; //{ get; set; }
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
            Close();
        }

        private void EnterBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //double result = Double.Parse((string)TotalLabel.Content) - Double.Parse(CashTotal);
            //CashTotal = String.Format("{0:#,###0.00}", result);
            Total = decimal.Parse((String)TotalLabel.Content);
            IsTransactionComplete = true;
            CancelBtn_MouseUp(sender, e);
        }
    }
}

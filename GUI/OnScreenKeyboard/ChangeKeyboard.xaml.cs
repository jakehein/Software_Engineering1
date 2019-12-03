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
        public ChangeKeyboard()
        {
            InitializeComponent();
            EnterBtn.Content = EnterText;
        }

        private const string EnterText = "E\nN\nT\nE\nR";

        private void Increase_PayTotal(string value)
        {
            string total = TotalLabel.Text.Remove(TotalLabel.Text.IndexOf('.'), 1);
            if (total[0] == '0')
            {
                total = total.Substring(1);
            }
            total += value;
            TotalLabel.Text = total.Insert(total.Length - 2, ".");
        }

        private void Decrease_PayTotal()
        {
            string total = TotalLabel.Text.Remove(TotalLabel.Text.IndexOf('.'), 1);
            total = total.Remove(total.Length - 1);
            if (total.Length < 3)
            {
                total = total.Insert(0, "0");
            }
            TotalLabel.Text = total.Insert(total.Length - 2, ".");
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
            ((TextBox)sender).FontSize = ((TextBox)sender).ActualHeight / 1.5;
        }

        //private void DeleteBtn_TouchUp(object sender, TouchEventArgs e)
        //{
        //    Decrease_PayTotal();
        //}

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

        }

        private void EnterBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        //private void Number_TouchUp(object sender, TouchEventArgs e)
        //{
        //    Increase_PayTotal(((Label)sender).Content.ToString());
        //}

    }
}

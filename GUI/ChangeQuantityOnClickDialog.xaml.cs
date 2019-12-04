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

namespace FinalProject1.GUI
{
    /// <summary>
    /// Interaction logic for ChangeQuantityOnClickDialog.xaml
    /// </summary>
    public partial class ChangeQuantityOnClickDialog : Window
    {
        private string _result = "";
        public string Result {
            get {return _result; } 
        }
        public ChangeQuantityOnClickDialog()
        {
            InitializeComponent();
            txtNewQuantity.Focus();
        }

        /// <summary>
        /// Adjust the font sizes with window resize
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResizeFonts(object sender, SizeChangedEventArgs e)
        {
            lblPrompt.FontSize = lblPrompt.ActualHeight * .7;
            txtNewQuantity.FontSize = txtNewQuantity.ActualHeight * .7;
        }

        /// <summary>
        /// Store the textbox value set result to true and close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            _result = txtNewQuantity.Text;
            DialogResult = true;
            Close();
        }

        /// <summary>
        /// Restrict text entry to numbers and set carretIndex to appropiate position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtNewQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!txtNewQuantity.Text.Equals(""))
            {
                if (!int.TryParse(txtNewQuantity.Text, out int parsedValue) || parsedValue < 0)
                {
                    int carretPos = txtNewQuantity.CaretIndex;
                    txtNewQuantity.Text = _result;
                    if (carretPos == 0)
                    {
                        txtNewQuantity.CaretIndex = 0;
                    }
                    else
                    {
                        txtNewQuantity.CaretIndex = carretPos > txtNewQuantity.Text.Length ? txtNewQuantity.Text.Length : carretPos - 1;
                    }
                }
                else
                {
                    _result = parsedValue + "";
                }
            }
            else
            {
                _result = "";
            }
        }
    }
}

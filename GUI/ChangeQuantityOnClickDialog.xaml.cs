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

        private void ResizeFonts(object sender, SizeChangedEventArgs e)
        {
            lblPrompt.FontSize = lblPrompt.ActualHeight * .7;
            txtNewQuantity.FontSize = txtNewQuantity.ActualHeight * .7;
        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            _result = txtNewQuantity.Text;
            DialogResult = true;
            Close();
        }

        private void TxtNewQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!float.TryParse(txtNewQuantity.Text, out float parsedValue))
            {
                int carretPos = txtNewQuantity.CaretIndex;
                txtNewQuantity.Text = _result;
                txtNewQuantity.CaretIndex = carretPos > txtNewQuantity.Text.Length ? txtNewQuantity.Text.Length : carretPos - 1;
            }
            else
            {
                _result = parsedValue + "";
            }
        }
    }
}

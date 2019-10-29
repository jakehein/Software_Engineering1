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
    /// Interaction logic for SalesPage.xaml
    /// </summary>
    public partial class SalesPage : Page
    {
        public SalesPage()
        {
            InitializeComponent();
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            //this.NavigationService.Navigate(new MainMenu());
            this.NavigationService.GoBack();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Pay_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void QuantityUp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void QuantityDown_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpCButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

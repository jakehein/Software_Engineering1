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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Initializes Login Page
            MainMenuUI.Content = new LoginPage();
        }

        /// <summary>
        /// Logout the original user and allow for a new user to sign in to the system.
        /// </summary>
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenuUI.Content = new LoginPage();
        }

        private void InventoryButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenuUI.Content = new InventoryPage();
        }

        private void SalesButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

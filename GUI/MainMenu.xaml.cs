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
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Logout the original user and allow for a new user to sign in to the system.
        /// </summary>
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Login_Page());
        }

        /// <summary>
        /// Navigates the user from the MainMenu page to the Inventory Page
        /// </summary>
        /// <param name="sender">Object that triggered the event</param>
        /// <param name="e">Event details</param>
        private void InventoryButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new InventoryPage());
        }

        /// <summary>
        /// Navigates the user from the MainMenu page to the Sales Page
        /// </summary>
        /// <param name="sender">Object that triggered the event</param>
        /// <param name="e">Event details</param>
        private void SalesButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SalesPage());
        }

        /// <summary>
        /// Navigates the user from the MainMenu page to the Return Page
        /// </summary>
        /// <param name="sender">Object that triggered the event</param>
        /// <param name="e">Event details</param>
        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ReturnPage());
        }

        /// <summary>
        /// Navigates the user from the MainMenu page to the CashTillPage
        /// </summary>
        /// <param name="sender">Object that triggered the event</param>
        /// <param name="e">Event details</param>
        private void CashButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CashTillPage());
        }
    }
}

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

namespace FinalProject1.GUI
{
    /// <summary>
    /// Interaction logic for Management.xaml
    /// </summary>
    public partial class Management : Page
    {
        IUserController userController => ControllerContainer.Instance.UserController;
        public Management()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Switch to the create user page
        /// </summary>
        /// <param name="sender">Object that triggered the event</param>
        /// <param name="e">Event details</param>
        private void CreateUserButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CreateUserPage(this.userController));
        }

        /// <summary>
        /// Navigates the user from the Management page to the Inventory Page
        /// </summary>
        /// <param name="sender">Object that triggered the event</param>
        /// <param name="e">Event details</param>
        private void InventoryButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new InventoryPage());
        }

        /// <summary>
        /// Navigates the user from the Management page to the MainMenu Page
        /// </summary>
        /// <param name="sender">Object that triggered the event</param>
        /// <param name="e">Event details</param>
        private void MainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainMenu());
        }
    }
}

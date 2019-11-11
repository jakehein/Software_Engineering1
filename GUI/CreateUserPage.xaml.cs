using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for CreateUserPage.xaml
    /// </summary>
    public partial class CreateUserPage : Page
    {
        private IUserController userController;

        public CreateUserPage(IUserController userController)
        {
            this.userController = userController;
            InitializeComponent();
        }

        private void CreateUserButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string pin = PasswordBox.Password;
            string reEnteredPin = ReEnterPasswordBox.Password;
            bool hasInventoryAccess = InventoryAccessCheckbox.IsChecked.Value;


            if(ValidateUsername(username))
            {
                if(ValidatePins(pin, reEnteredPin))
                {
                    userController.CreateUser(username, pin, hasInventoryAccess);
                    this.NavigationService.Navigate(new MainMenu());
                }
                else
                {
                    ErrorLabel.Content = "Invalid Pin Entered, must contain only numbers and be between 5 and 11 characters in length";
                }
            }
            else
            {
                ErrorLabel.Content = "Invalid Username Entered, must be between 5 and 50 characters in length";
            }

        }

        private bool ValidateUsername(string username)
        {
            bool isValid = false;
            if(username.Length >= 5 && username.Length <= 50)
            {
                isValid = true;
            }

            return isValid;
        }

        private bool ValidatePins(string pin, string reEnteredPin)
        {
            bool isValid = false;
            if (Regex.Matches(pin, @"^[0-9]{5,11}$").Count == 1 && pin == reEnteredPin)
            {
                isValid = true;
            }

            return isValid;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Login_Page());
        }
    }
}

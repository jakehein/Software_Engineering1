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
        private IUserController userController => ControllerContainer.Instance.UserController;

        public CreateUserPage(IUserController userController)
        {
            InitializeComponent();
            UsernameBox.Focus();
        }

        private void CreateUserButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string password = PasswordBox.Password;
            string reEnteredPasssword = ReEnterPasswordBox.Password;
            bool hasManagerAccess = ManagerAccessCheckbox.IsChecked.Value;


            if(ValidateUsername(username))
            {
                if(userController.DoesUserExist(username))
                {
                    ErrorLabel.Content = "Username is already taken";
                }
                else if(ValidatePassword(password, reEnteredPasssword))
                {
                    userController.CreateUser(username, password, hasManagerAccess);
                    ClearFields();
                    ErrorLabel.Content = "User successfully created";
                    //this.NavigationService.Navigate(new CashTillStartPage());
                }
                else
                {
                    ErrorLabel.Content = "Invalid Password Entered, must contain only numbers and letters and be between 5 and 20 characters in length";
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

        private bool ValidatePassword(string password, string reEnteredPassword)
        {
            bool isValid = false;
            if (Regex.Matches(password, @"^.*(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).*$").Count == 1 && password.Length >= 5 && password.Length <= 20 && password == reEnteredPassword)
            {
                isValid = true;
            }

            return isValid;
        }

        /// <summary>
        /// Clear out the textboxes.
        /// </summary>
        private void ClearFields()
        {
            UsernameBox.Text = "";
            PasswordBox.Password = "";
            ReEnterPasswordBox.Password = "";
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new GUI.Management());
        }

        private void UsernameBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((TextBox)sender).FontSize = ((TextBox)sender).ActualHeight * .7;
        }

        private void PasswordBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((PasswordBox)sender).FontSize = ((PasswordBox)sender).ActualHeight * .7;
        }

        private void ReEnterPasswordBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((PasswordBox)sender).FontSize = ((PasswordBox)sender).ActualHeight * .7;
        }

        private void UsernameBox_TouchUp(object sender, TouchEventArgs e)
        {
            GUI.OnScreenKeyboard.OnScreenKeyboard keyboard = new GUI.OnScreenKeyboard.OnScreenKeyboard(GUI.OnScreenKeyboard.OnScreenKeyboard.InputType.TEXT, Window.GetWindow(this), UsernameBox.Text);
            bool? dialogResult = keyboard.ShowDialog();
            if (dialogResult != null && (bool)dialogResult)
            {
                UsernameBox.Text = keyboard.GetResult();
                UsernameBox.CaretIndex = UsernameBox.Text.Length;
            }
        }

        private void PasswordBox_TouchUp(object sender, TouchEventArgs e)
        {
            GUI.OnScreenKeyboard.OnScreenKeyboard keyboard = new GUI.OnScreenKeyboard.OnScreenKeyboard(GUI.OnScreenKeyboard.OnScreenKeyboard.InputType.PASSWORD, Window.GetWindow(this), PasswordBox.Password);
            bool? dialogResult = keyboard.ShowDialog();
            if (dialogResult != null && (bool)dialogResult)
            {
                PasswordBox.Password = keyboard.GetResult();
            }
        }

        private void ReEnterPasswordBox_TouchUp(object sender, TouchEventArgs e)
        {
            GUI.OnScreenKeyboard.OnScreenKeyboard keyboard = new GUI.OnScreenKeyboard.OnScreenKeyboard(GUI.OnScreenKeyboard.OnScreenKeyboard.InputType.PASSWORD, Window.GetWindow(this), ReEnterPasswordBox.Password);
            bool? dialogResult = keyboard.ShowDialog();
            if (dialogResult != null && (bool)dialogResult)
            {
                ReEnterPasswordBox.Password = keyboard.GetResult();
            }
        }
    }
}

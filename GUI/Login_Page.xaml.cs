
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
    /// Interaction logic for Login_Page.xaml
    /// </summary>
    public partial class Login_Page : Page
    {
        IUserController userController = ControllerContainer.Instance.UserController;
        public Login_Page()
        {
            InitializeComponent();
            UsernameBox.Focus();
        }

        /// <summary>
        /// Within this method we would like to verify the username and password against the database of
        /// avaliable users. If the user is valid then we move to the Main Menu of the register. If not
        /// we display an "Invalid credentials" error to the user and allow them to try again.
        /// </summary>
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // get the username and password and store as a string to check against database of users
            string username = UsernameBox.Text;
            string pin = PasswordBox.Password.ToString();

            string errors = userController.LogIn(username, pin);

            if(errors.Length > 0)
            {
                errorLabel.Content = errors;
            }
            else
            {
                this.NavigationService.Navigate(new CashTillStartPage());
               // this.NavigationService.Navigate(new s)
            }
            // at this point it does not validate the legitimacy of the user but rather 
            // forwards it onto the MainMenu page

            // Close Current Window... This will be done after validating if the credentials are good or not
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
        /// resize the username box on screen size change
        /// </summary>
        /// <param name="sender">Object that triggered the event</param>
        /// <param name="e">Event details</param>
        private void UsernameBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((TextBox)sender).FontSize = ((TextBox)sender).ActualHeight * .7;
        }

        /// <summary>
        /// Resize the password box on screen size change
        /// </summary>
        /// <param name="sender">Object that triggered the event</param>
        /// <param name="e">Event details</param>
        private void PasswordBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((PasswordBox)sender).FontSize = ((PasswordBox)sender).ActualHeight * .7;
        }

        /// <summary>
        /// Show the onscreen keyboard for the user name field
        /// </summary>
        /// <param name="sender">Object that triggered the event</param>
        /// <param name="e">Event details</param>
        private void EnableTouchKeyboardForUserName(object sender, TouchEventArgs e)
        {
            GUI.OnScreenKeyboard.OnScreenKeyboard keyboard = new GUI.OnScreenKeyboard.OnScreenKeyboard(GUI.OnScreenKeyboard.OnScreenKeyboard.InputType.TEXT, Window.GetWindow(this), UsernameBox.Text);
            keyboard.ShowDialog();
            UsernameBox.Text = keyboard.GetResult();
            UsernameBox.CaretIndex = UsernameBox.Text.Length;
        }

        /// <summary>
        /// Show the onscreen keyboard for the password fields input
        /// </summary>
        /// <param name="sender">Object that triggered the event</param>
        /// <param name="e">Event details</param>
        private void EnableTouchKeyboardForPassword(object sender, TouchEventArgs e)
        {
            GUI.OnScreenKeyboard.OnScreenKeyboard keyboard = new GUI.OnScreenKeyboard.OnScreenKeyboard(GUI.OnScreenKeyboard.OnScreenKeyboard.InputType.PASSWORD, Window.GetWindow(this), PasswordBox.Password);
            keyboard.ShowDialog();
            PasswordBox.Password = keyboard.GetResult();
        }
    }
}


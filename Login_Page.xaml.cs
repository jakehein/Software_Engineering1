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
        public Login_Page()
        {
            InitializeComponent();
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
            string password = PasswordBox.Password.ToString();
            //Boolean loginValidity = false;

            // at this point it does not validate the legitimacy of the user but rather 
            // forwards it onto the MainMenu page

            // Close Current Window... This will be done after validating if the credentials are good or not
            Window.GetWindow(this).Close();
        }
    }
}


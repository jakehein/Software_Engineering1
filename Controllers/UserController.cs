using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    class UserController : IUserController
    {
        private IUserDataAccess userDataAccess => DataAccessContainer.Instance.UserDataAccess;

        public User CurrentUser { get; private set; }

        /// <summary>
        /// Checks if the currently logged in user has manager access
        /// </summary>
        /// <returns></returns>
        public bool CurrentUserHasManagerAccess()
        {
            return CurrentUser.HasManagerAccess;
        }

        /// <summary>
        /// Attemps to log in a user
        /// </summary>
        /// <param name="userName">The username of a user to attempt to log in</param>
        /// <param name="password">The password of a user to attempt to log in</param>
        /// <returns>A list of errors that occured while trying to log the user in,
        /// if this list is empty, it can be assumed that the user was successfully logged in</returns>
        public string LogIn(string username, string password)
        {
            string errorMessage = "";
            //string retrivedPassword = userDataAccess.GetPassword(username);
            if(!userDataAccess.ValidateUser(username, password))
            //if (retrivedPassword != password)
            {
                errorMessage = "Invalid username or password";
            }
            else
            {
                CurrentUser = new User() { Username = username, Password = password, HasManagerAccess = userDataAccess.DoesUserHaveInvetoryAccess(username) };
            }
            return errorMessage;

        }

        /// <summary>
        /// Creates a user with the given properties
        /// </summary>
        /// <param name="username">The username of the user to create</param>
        /// <param name="password">The password of the user to create</param>
        /// <param name="hasManagerAccess">If the user to create should have manager access</param>
        /// <returns></returns>
        public bool CreateUser(string username, string password, bool hasManagerAccess)
        {
            bool validUser = false;
            User newUser = new User() { Username = username, Password = password, HasManagerAccess = hasManagerAccess };

            if(newUser.DataErrors.Count == 0)
            {
                validUser = true;
                userDataAccess.CreateUser(newUser.Username, newUser.Password, newUser.HasManagerAccess);
                //this.CurrentUser = newUser;
            }

            return validUser;
        }

        /// <summary>
        /// Checks if a user exists that has the given username
        /// </summary>
        /// <param name="username">The username to check</param>
        /// <returns>True if the user exists, otherwise false</returns>
        public bool DoesUserExist(string username)
        { 
            return userDataAccess.DoesUserExist(username);
        }

        /// <summary>
        /// Logs out the currently logged in user
        /// </summary>
        public void LogOut()
        {
            CurrentUser = null;
        }

        //TODO: figure out if we are keeping this
        //public bool Override(string password)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

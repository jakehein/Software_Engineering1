using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    interface IUserDataAccess
    {
        /// <summary>
        /// Creates a user with the given username, password, and manager access status
        /// </summary>
        /// <param name="username">The username to give the created user</param>
        /// <param name="password">The password to give the created user</param>
        /// <param name="hasManagerAccess">The manager status to give the created user</param>
        void CreateUser(string username, string password, bool hasManagerAccess);

        /// <summary>
        /// Gets the password for the user with the given username
        /// </summary>
        /// <param name="username">The username of the user to get the password for</param>
        /// <returns>The string representing the password of the user</returns>
        string GetPassword(string username);

        /// <summary>
        /// Checks if a user exists with the given username
        /// </summary>
        /// <param name="username">The username to check</param>
        /// <returns>True if a user exists, otherwise false</returns>
        bool DoesUserExist(string username);

        /// <summary>
        /// Checks if the user with the given username has manager access
        /// </summary>
        /// <param name="username">The username ot check</param>
        /// <returns>True if the user has manager access, otherwise false</returns>
        bool DoesUserHaveManagerAccess(string username);

        /// <summary>
        /// Updates the password for the given user
        /// </summary>
        /// <param name="username">The username of the user to update</param>
        /// <param name="password">The new password to set</param>
        void UpdatePassword(string username, string pin);

        /// <summary>
        /// Get the hashed password for the username entered and return if they match
        /// </summary>
        /// <param name="username">User entered username</param>
        /// <param name="password">User entered password</param>
        /// <returns>True if the user entered password and hashed password for the user from the DB are equal</returns
        bool ValidateUser(string username, string password);
    }
}

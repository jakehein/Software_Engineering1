using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    public interface IUserController
    {
        User CurrentUser { get; }

        /// <summary>
        /// Attemps to log in a user
        /// </summary>
        /// <param name="userName">The username of a user to attempt to log in</param>
        /// <param name="password">The password of a user to attempt to log in</param>
        /// <returns>A list of errors that occured while trying to log the user in,
        /// if this list is empty, it can be assumed that the user was successfully logged in</returns>
        string LogIn(string userName, string password);

        /// <summary>
        /// Creates a user with the given properties
        /// </summary>
        /// <param name="username">The username of the user to create</param>
        /// <param name="password">The password of the user to create</param>
        /// <param name="hasManagerAccess">If the user to create should have manager access</param>
        /// <returns></returns>
        bool CreateUser(string username, string password, bool hasManagerAccess);

        /// <summary>
        /// Checks if a user exists that has the given username
        /// </summary>
        /// <param name="username">The username to check</param>
        /// <returns>True if the user exists, otherwise false</returns>
        bool DoesUserExist(string username);
        
        /// <summary>
        /// Checks if the currently logged in user has manager access
        /// </summary>
        /// <returns></returns>
        bool CurrentUserHasManagerAccess();

        /// <summary>
        /// Logs out the currently logged in user
        /// </summary>
        void LogOut();

    }
}

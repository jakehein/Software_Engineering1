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

        public bool CurrentUserHasManagerAccess()
        {
            return CurrentUser.HasManagerAccess;
        }

        public string LogIn(string username, string password)
        {
            string errorMessage = "";
            string retrivedPassword = userDataAccess.GetPassword(username);
            if (retrivedPassword != password)
            {
                errorMessage = "Invalid username or password";
            }
            else
            {
                CurrentUser = new User() { Username = username, Password = password, HasManagerAccess = userDataAccess.DoesUserHaveInvetoryAccess(username) };
            }
            return errorMessage;

        }

        public bool CreateUser(string username, string password, bool hasManagerAccess)
        {
            bool validUser = false;
            User newUser = new User() { Username = username, Password = password, HasManagerAccess = hasManagerAccess };

            if(newUser.DataErrors.Count == 0)
            {
                validUser = true;
                userDataAccess.CreateUser(newUser.Username, newUser.Password, newUser.HasManagerAccess);
                this.CurrentUser = newUser;
            }

            return validUser;
        }

        public bool DoesUserExist(string username)
        { 
            return userDataAccess.DoesUserExist(username);
        }

        public void LogOut()
        {
            CurrentUser = null;
        }

        public bool Override(string password)
        {
            throw new NotImplementedException();
        }
    }
}

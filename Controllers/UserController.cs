using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    class UserController : IUserController
    {
        private IUserDataAccess userDataAccess = new UserDataAccess();

        public User CurrentUser { get; private set; }

        public bool CurrentUserHasInventoryAccess()
        {
            return CurrentUser.HasInventoryAccess;
        }

        public string LogIn(string username, string pin)
        {
            string errorMessage = "";
            string retrivedPIN = userDataAccess.GetPin(username);
            if (retrivedPIN != pin)
            {
                errorMessage = "Invalid username or password";
            }
            else
            {
                CurrentUser = new User() { Username = username, PIN = pin, HasInventoryAccess = userDataAccess.DoesUserHaveInvetoryAccess(username) };
            }
            return errorMessage;

        }

        public bool CreateUser(string username, string pin, bool hasInventoryAccess)
        {
            bool validUser = false;
            User newUser = new User() { Username = username, PIN = pin, HasInventoryAccess = hasInventoryAccess };

            if(newUser.DataErrors.Count == 0)
            {
                validUser = true;
                userDataAccess.CreateUser(newUser.Username, newUser.PIN, newUser.HasInventoryAccess);
                this.CurrentUser = newUser;
            }

            return validUser;
        }

        public void LogOut()
        {
            CurrentUser = null;
        }

        public bool Override(string pin)
        {
            throw new NotImplementedException();
        }
    }
}

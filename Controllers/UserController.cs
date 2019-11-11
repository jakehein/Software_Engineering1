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

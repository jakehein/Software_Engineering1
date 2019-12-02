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

        string LogIn(string userName, string password);

        bool Override(string pin);

        bool CreateUser(string username, string password, bool hasInventoryAccess);
        bool DoesUserExist(string username);
        bool CurrentUserHasInventoryAccess();

        void LogOut();

    }
}

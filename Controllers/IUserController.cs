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

        string LogIn(string userName, string pin);

        bool Override(string pin);

        bool CreateUser(string username, string pin, bool hasInventoryAccess);

        bool CurrentUserHasInventoryAccess();

        void LogOut();

    }
}

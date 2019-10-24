using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    interface IUserController
    {
        UserDTO CurrentUser { get; }

        string LogIn(string userName, string password);

        bool Override(string pin);
            
        string LogOut();

    }
}

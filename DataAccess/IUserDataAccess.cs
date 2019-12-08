using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    interface IUserDataAccess
    {
        void CreateUser(string username, string password, bool hasManagerAccess);
        string GetPassword(string username);
        bool DoesUserExist(string username);
        bool DoesUserHaveInvetoryAccess(string username);
        void UpdatePassword(string username, string pin);

        bool ValidateUser(string username, string password);
    }
}

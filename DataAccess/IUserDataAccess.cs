using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1.DataAccess
{
    interface IUserDataAccess
    {
        void CreateUser(string username, string pin, bool hasInventoryAccess);
        void GetPassword(string username);
        void UpdatePassword(string username, string password);
    }
}

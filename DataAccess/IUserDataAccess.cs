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
        string GetPin(string username);
        bool DoesUserHaveInvetoryAccess(string username);
        void UpdatePin(string username, string pin);
    }
}

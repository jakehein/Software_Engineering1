using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace FinalProject1
{
    class InventoryDataAccess
    {
        bool CreateItem(Item item)
        {

            //throw new NotImplementedException();
            return false;
        }

        bool UpdateItem(string pLU, Item item)
        {
            throw new NotImplementedException();
            return false;
        }

        bool DeleteItem(string pLU)
        {
            throw new NotImplementedException();
            return false;
        }

        Item GetItem(string pLU)
        {
            throw new NotImplementedException();
            return null;
            //return item;
        }

        List<Item> GetAllItems()
        {
            throw new NotImplementedException();
            return null;
            //return itemList;
        }
    }
}

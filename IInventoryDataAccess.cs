using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    interface IInventoryDataAccess
    {
        bool CreateItem(Item item);

        bool UpdateItem(string uPC, Item item);

        bool DeleteItem(string uPC);

        List<string> GetAllUPCs();

        Item GetItem(string uPC);

        List<Item> GetAllItems();
    }
}

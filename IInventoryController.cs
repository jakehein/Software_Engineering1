using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    public interface IInventoryController
    {
        bool CreateItem(Item item);

        bool UpdateItem(string pLU, Item item);

        bool DeleteItem(string pLU);

        Item GetItem(string pLU);

        List<Item> GetAllItems();
    }
}

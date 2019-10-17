using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    interface IInventoryController
    {
        bool CreateItem(ItemDTO item);

        bool UpdateItem(string pLU, ItemDTO item);

        bool DeleteItem(string pLU);

        List<string> AllPLUs { get; }

        ItemDTO GetItem(string pLU);

        List<ItemDTO> GetAllItems();
    }
}

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

        bool UpdateItem(string uPC, ItemDTO item);

        bool DeleteItem(string uPC);

        List<string> AllUPCs { get; }

        ItemDTO GetItem(string uPC);

        List<ItemDTO> GetAllItems();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    interface ICartController
    {
        bool AddItem(ItemDTO item);

        bool ChangeQuantity(int quantity, ItemDTO item);

        bool RemoveItem(ItemDTO item);

        //List<string> AllUPCs { get; }

        //ItemDTO GetItem(string uPC);

        List<ItemDTO> GetAllItems();

        decimal? GetTotal { get; }

        void Checkout();

        void CancelTransaction();
    }
}

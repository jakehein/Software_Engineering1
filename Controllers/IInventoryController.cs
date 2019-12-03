using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    interface IInventoryController
    {
        /// <summary>
        ///  This method checks to see if an item is valid for insertion into the iDA.
        ///  If so,the item is stored into iDA via a call to CreateItem();
        /// </summary>
        /// <param name="item">item object being created in iDA</param>
        /// <returns>boolean detailing if operation was carried out</returns>
        bool CreateItem(ItemDTO item);

        bool UpdateItem(string uPC, ItemDTO item);

        /// <summary>
        /// This method checks to see if an item is valid for deletion.
        /// If so, the item is deleted.
        /// </summary>
        /// <param name="uPC">UPC string value being checked for before carrying out deletion</param>
        /// <returns>boolean detailing if operation was carried out</returns>
        bool DeleteItem(string uPC);

        List<string> AllUPCs {get;}

        ItemDTO GetItem(string uPC);

        /// <summary>
        /// This method creates a list of all Item objects contained in iDA
        /// </summary>
        /// <returns>List<Item> of all Item objects contained in iDA</returns>
        List<ItemDTO> GetAllItems();

        List<ItemDTO> GetAllItemsFromCategory(long iD);

        bool UpdateItemCategory(ItemDTO item, CategoryDTO category);
    }
}

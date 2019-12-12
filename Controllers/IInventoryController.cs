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


        /**
         * This method checks to see if a pLU and item are valid input for update into iDA. If so,
         * the item is updated into iDA via a call to UpdateItem();
         * @param UPC of item to be updated
         * @param item object being updated
         * @return boolean detailing if operation was carried out
         */
        bool UpdateItem(string uPC, ItemDTO item);

        /// <summary>
        /// This method checks to see if an item is valid for deletion.
        /// If so, the item is deleted.
        /// </summary>
        /// <param name="uPC">UPC string value being checked for before carrying out deletion</param>
        /// <returns>boolean detailing if operation was carried out</returns>
        bool DeleteItem(string uPC);

        /// <summary>
        /// This method gets and returns all UPCs in the system
        /// </summary>
        /// <returns> List<string> containing all UPC</returns>
        List<string> AllUPCs {get;}

        /// <summary>
        /// This method creates a list of all Item objects contained in iDA
        /// </summary>
        /// <returns>List<Item> of all Item objects contained in iDA</returns>
        ItemDTO GetItem(string uPC);

        /// <summary>
        /// This method creates a list of all Item objects contained in iDA
        /// </summary>
        /// <returns>List<Item> of all Item objects contained in iDA</returns>
        List<ItemDTO> GetAllItems();

        /// <summary>
        /// This method gets all items from a specified category.
        /// </summary>
        /// <param name="iD">iD of the wanted category</param>
        /// <returns>List of ItemDTOs that represents objects within a category</returns>
        List<ItemDTO> GetAllItemsFromCategory(long iD);

        /// <summary>
        /// This method updates a specificed item with a new category
        /// </summary>
        /// <param name="item">itemDTO of the item we are trying to update</param>
        /// <param name="category">is the category to update the item to</param>
        /// <returns></returns>
        bool UpdateItemCategory(ItemDTO item, CategoryDTO category);
    }
}

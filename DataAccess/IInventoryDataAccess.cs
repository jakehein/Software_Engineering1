using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    interface IInventoryDataAccess
    {
        /// <summary>
        /// Add a new Item to the database if it doesn't already exist
        /// </summary>
        /// <param name="item">Item to be added</param>
        /// <returns>True if Item added successfully</returns>
        bool CreateItem(ItemDTO item);

        /// <summary>
        /// Update the values of a passed in Item
        /// </summary>
        /// <param name="uPC">Original UPC of item</param>
        /// <param name="item">Item with new values</param>
        /// <returns>True if Item updated</returns>
        bool UpdateItem(string uPC, ItemDTO item);

        /// <summary>
        /// Delete an Item with the passed in UPC from the database
        /// </summary>
        /// <param name="uPC">UPC of Item to be deleted</param>
        /// <returns>True if Item deleted</returns>
        bool DeleteItem(string uPC);

        /// <summary>
        /// Get all UPCs stored in the database
        /// </summary>
        /// <returns>List containing all UPCs</returns>
        List<string> GetAllUPCs();

        /// <summary>
        /// Get Item corresponding with passed in UPC
        /// </summary>
        /// <param name="uPC">Item UPC</param>
        /// <returns>Item associated with UPC</returns>
        ItemDTO GetItem(string uPC);

        /// <summary>
        /// Get all Items stored in the database
        /// </summary>
        /// <returns>List containing all Items</returns>
        List<ItemDTO> GetAllItems();

        /// <summary>
        /// Updates the quantity of the item with the given UPC
        /// </summary>
        /// <param name="uPC">The UPC of the item to Update</param>
        /// <param name="amount">The new amount to set</param>
        /// <returns>True if the item was updated, otherwise false</returns>
        bool ChangeQuantity(string uPC, int amount);

        /// <summary>
        /// Get all items in a specific category specified by the ID
        /// </summary>
        /// <param name="iD">specified category ID</param>
        /// <returns>List containing all Items with a categoryiD of iD</returns>
        List<ItemDTO> GetAllItemsFromCategory(long iD);

        /// <summary>
        /// This method updates the passed items category to the category specified in 'category' variable
        /// </summary>
        /// <param name="item">item thats category is being updated</param>
        /// <param name="category">is the category that is being updated to</param>
        /// <returns></returns>
        bool UpdateItemCategory(ItemDTO item, CategoryDTO category);
    }
}

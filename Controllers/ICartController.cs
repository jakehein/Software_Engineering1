using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    interface ICartController
    {
        /// <summary>
        /// Add a single item to the cart.
        /// </summary>
        /// <param name="item">The item to add to the cart</param>
        /// <returns>true if the item is successfully added</returns>
        bool AddItem(ItemDTO item);

        /// <summary>
        /// Change the quantity of an item in the cart
        /// </summary>
        /// <param name="quantity">quantity to set for item</param>
        /// <param name="item">item to change quantity of</param>
        /// <returns>true if quantity changed successfully</returns>
        bool ChangeQuantity(int quantity, ItemDTO item);

        /// <summary>
        /// Remove an item from the cart
        /// </summary>
        /// <param name="item">Item to remove from the cart</param>
        /// <returns>true if item is removed successfully</returns>
        bool RemoveItem(ItemDTO item);

        //List<string> AllUPCs { get; }

        /// <summary>
        /// This method takes a UPC and returns the ItemDTO
        /// </summary>
        /// <param name="uPC"> is the name of the UPC being queried</param>
        /// <returns>ItemDTO of the item with the specified UPC</returns>
        ItemDTO GetItem(string uPC);

        //List<ItemDTO> GetAllItems();

        /// <summary>
        /// Get all the items contained in the cart in a List
        /// </summary>
        /// <returns>List<ItemDTO> containing all cart items</returns>
        List<DTOs.SalesItemDTO> GetAllItems();

        /// <summary>
        /// Gets the total of all items in the cart
        /// </summary>
        /// <returns>The total of all items in the cart</returns>
        decimal? GetTotal();

        /// <summary>
        /// Checks out the cart of items
        /// </summary>
        /// <returns>An empty list of item DTOs</returns>
        List<ItemDTO> Checkout();

        /// <summary>
        /// Returns the items currently in the cart
        /// </summary>
        /// <returns></returns>
        List<ItemDTO> ReturnItems();

        /// <summary>
        /// Ends the current transaction and refreshes the cart
        /// <returns>List<ItemDTO> containing a new empty list</returns>
        /// </summary>
        List<ItemDTO> CancelTransaction();
    }
}

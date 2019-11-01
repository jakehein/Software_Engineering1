using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    class CartController : ICartController
    {
        Cart cart;
        
        IInventoryDataAccess inventoryDataAccess = new InventoryDataAccess(new CategoryDataAccess());
        public decimal? GetTotal { get; private set; }

        /// <summary>
        /// Create an instance of the CartController
        /// </summary>
        public CartController()
        {
            cart = new Cart();
        }

        /// <summary>
        /// Add a single item to the cart.
        /// </summary>
        /// <param name="item">The item to add to the cart</param>
        /// <returns>true if the item is successfully added</returns>
        public bool AddItem(ItemDTO itemDTO)
        {
            bool success = false;

            Item item = Item.createItemFromDTO(itemDTO);
            if(item.DataWarnings.Count == 0)
            {
                cart.AddItem(item);
                success = true;
            }

            return success;
        }

        /// <summary>
        /// Change the quantity of an item in the cart
        /// </summary>
        /// <param name="quantity">quantity to set for item</param>
        /// <param name="item">item to change quantity of</param>
        /// <returns>true if quantity changed successfully</returns>
        public bool ChangeQuantity(int quantity, ItemDTO itemDTO)
        {
            bool success = false;

            Item item = Item.createItemFromDTO(itemDTO);
            if (item.DataWarnings.Count == 0)
            {
                success = cart.ChangeQuantity(quantity, item);
            }

            return success;
        }

        /// <summary>
        /// Remove an item from the cart
        /// </summary>
        /// <param name="item">Item to remove from the cart</param>
        /// <returns>true if item is removed successfully</returns>
        public bool RemoveItem(ItemDTO itemDTO)
        {
            bool success = false;

            Item item = Item.createItemFromDTO(itemDTO);
            if (item.DataWarnings.Count == 0)
            {
                success = cart.RemoveSingleItem(item);
            }

            return success;
        }

        /// <summary>
        /// This method takes a UPC and returns the ItemDTO
        /// </summary>
        public ItemDTO GetItem(string uPC)
        {
            return inventoryDataAccess.GetItem(uPC);
        }

        /// <summary>
        /// Get all the items contained in the cart in a List
        /// </summary>
        /// <returns>List<ItemDTO> containing all cart items</returns>
        public List<ItemDTO> GetAllItems()
        {
            List<ItemDTO> items = new List<ItemDTO>();
            foreach(KeyValuePair<Item, int> kvp in cart.Items)
            {
                for(int i = 0; i < kvp.Value; i++)
                {
                    items.Add(Item.createDTOfromItem(kvp.Key));
                }
            }
            return items;
        }

        /// <summary>
        /// Checks out the cart of items
        /// </summary>
        public void Checkout()
        {
            cart.Checkout(inventoryDataAccess);
            cart = new Cart();
        }

        public void CancelTransaction()
        {
            cart = new Cart();
        }
    }
}

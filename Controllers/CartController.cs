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

        IInventoryDataAccess inventoryDataAccess = DataAccessContainer.Instance.InventoryDataAccess;
        public decimal? GetTotal()
        {
            return cart.TotalCost;
        }

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
            if (item.DataWarnings.Count == 0)
            {
                cart.AddItem(itemDTO);
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
                success = cart.ChangeQuantity(quantity, itemDTO);
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
                success = cart.RemoveAllItems(itemDTO);
            }

            return success;
        }
        /// <summary>
        /// This method returns true if CartController contains itemDTO
        /// </summary>
        /// <param name="itemDTO">Item being checked</param>
        /// <returns> true if the item is contained</returns>
        public bool ContainsItem(ItemDTO itemDTO)
        {
            List<ItemDTO> items = new List<ItemDTO>();
            bool contains = items.Contains(itemDTO);
            return contains;
        }

        /// <summary>
        /// This method takes a UPC and returns the ItemDTO
        /// </summary>
        /// <param name="uPC"> is the name of the UPC being queried</param>
        /// <returns>ItemDTO of the item with the specified UPC</returns>
        public ItemDTO GetItem(string uPC)
        {
            return inventoryDataAccess.GetItem(uPC);
        }

        /// <summary>
        /// Get all the items contained in the cart in a List
        /// </summary>
        /// <returns>List<ItemDTO> containing all cart items</returns>
        public List<Cart.SalesItem> GetAllItems()
        //public Dictionary<Item, int> GetAllItems()
        {
            //List<ItemDTO> items = new List<ItemDTO>();
            //foreach(KeyValuePair<Item, int> kvp in cart.Items)
            //{
            //    for(int i = 0; i < kvp.Value; i++)
            //    {
            //        items.Add(Item.createDTOfromItem(kvp.Key));
            //    }
            //}
            //return items;
            List<Cart.SalesItem> items = new List<Cart.SalesItem>();
            foreach (Cart.SalesItem si in cart.Items)
            {
                items.Add(new Cart.SalesItem { Item = si.Item, Quantity = si.Quantity });
            }
            return items;
        }

        /// <summary>
        /// Checks out the cart of items
        /// </summary>
        public List<ItemDTO> Checkout()
        {
            cart.Checkout(inventoryDataAccess);
            cart = new Cart();
            return new List<ItemDTO>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ItemDTO> ReturnItems()
        {
            cart.ReturnItems(inventoryDataAccess);
            cart = new Cart();
            return new List<ItemDTO>();
        }

        /// <summary>
        /// Ends the current transaction and refreshes the cart
        /// <returns>List<ItemDTO> containing a new empty list</returns>
        /// </summary>
        public List<ItemDTO> CancelTransaction()
        {
            cart = new Cart();
            return new List<ItemDTO>();
        }
    }
}

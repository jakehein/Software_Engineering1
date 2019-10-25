using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    class CartController : ICartController
    {
        Dictionary<ItemDTO, int> cart = null;
        public decimal? GetTotal { get; private set; }

        /// <summary>
        /// Create an instance of the CartController
        /// </summary>
        public CartController()
        {
            cart = new Dictionary<ItemDTO, int>();
            GetTotal = 0.0M;
        }

        /// <summary>
        /// Add a single item to the cart.
        /// </summary>
        /// <param name="item">The item to add to the cart</param>
        /// <returns>true if the item is successfully added</returns>
        public bool AddItem(ItemDTO item)
        {
            // if the cart already contains an instance of the item the quantity is incremented
            if (cart.ContainsKey(item))
            {
                cart.TryGetValue(item, out int value);
                cart[item] = value++;
            }
            else // else add an instance
            {
                cart.Add(item, 1);
            }
            GetTotal += item.Price;
            return true;
        }

        /// <summary>
        /// Change the quantity of an item in the cart
        /// </summary>
        /// <param name="quantity">quantity to set for item</param>
        /// <param name="item">item to change quantity of</param>
        /// <returns>true if quantity changed successfully</returns>
        public bool ChangeQuantity(int quantity, ItemDTO item)
        {
            if (quantity == 0)
            {
                RemoveItem(item);
            }
            else
            {
                if (cart.ContainsKey(item))
                {
                    GetTotal += (quantity - cart[item]) * item.Price;
                    cart[item] = quantity;
                }
                else
                {
                    cart.Add(item, quantity);
                    GetTotal += quantity * item.Price;
                }
            }
            return true;
        }

        /// <summary>
        /// Remove an item from the cart
        /// </summary>
        /// <param name="item">Item to remove from the cart</param>
        /// <returns>true if item is removed successfully</returns>
        public bool RemoveItem(ItemDTO item)
        {
            if (cart.ContainsKey(item))
            {
                GetTotal -= item.Price * cart[item];
                cart.Remove(item);

            }
            return true;
        }

        //ItemDTO GetItem(string uPC)
        //{
        //    return null;
        //}

        /// <summary>
        /// Get all the items contained in the cart in a List
        /// </summary>
        /// <returns>List<ItemDTO> containing all cart items</returns>
        public List<ItemDTO> GetAllItems()
        {
            List<ItemDTO> items = new List<ItemDTO>();
            foreach(KeyValuePair<ItemDTO, int> kvp in cart)
            {
                for(int i = 0; i < kvp.Value; i++)
                {
                    items.Add(kvp.Key);
                }
            }
            return items;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Checkout()
        {
            //Remove items from inventory?
            //Add items to the sales table?
            DeleteCart();
        }

        /// <summary>
        /// empty the cart and reset the cost total
        /// </summary>
        public void DeleteCart()
        {
            cart.Clear();
            GetTotal = 0.0M;
        }
    }
}

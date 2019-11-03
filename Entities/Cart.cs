using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    class Cart
    {
        public Dictionary<Item, int> Items { get; set; }

        /// <summary>
        /// The total cost of all Items in the Cart
        /// </summary>
        public decimal TotalCost 
        {
            get
            {
                return Items.Select(x => x.Key.Price * x.Value).Sum();
            }
        }

        /// <summary>
        /// Adds an item to the cart
        /// </summary>
        /// <param name="item">The item to be added</param>
        public void AddItem(Item item)
        {
            /* //ORIGINAL
            int currentCount = 0;
            Items.TryGetValue(item, out currentCount);
            Items.Add(item, currentCount + 1); // ORIGINAL*/

            //TEST
            int currentCount = 0;
            bool result = Items.TryGetValue(item, out currentCount);
            if (result)
            {
                Items.Add(item, currentCount + 1);
            }
            else
            {
                //MessageBox.Show("eek");
            }
            
            //TEST
        }

        /// <summary>
        /// Removes one of the given item from the cart
        /// </summary>
        /// <param name="item">The item to be removed</param>
        /// <returns>True if an item was remvoed otherwise false</returns>
        public bool RemoveSingleItem(Item item)
        {
            bool itemRemoved = false;
            int count;
            if(Items.TryGetValue(item, out count))
            {
                if(count > 1)
                {
                    Items.Add(item, count - 1);
                }
                else
                {
                    Items.Remove(item);
                }
                itemRemoved = true;
            }

            return itemRemoved;
        }

        /// <summary>
        /// Removes all of a given item from the cart
        /// </summary>
        /// <param name="item">The item to remove all of</param>
        /// <returns>True if items were removed, otherwise false</returns>
        public bool RemoveAllItems(Item item)
        {
            bool itemsRemoved = false;
            if (Items.ContainsKey(item))
            {
                Items.Remove(item);
                itemsRemoved = true;
            }
            return itemsRemoved;
        }

        public bool ChangeQuantity(int quantity, Item item)
        {
            if (quantity == 0)
            {
                RemoveAllItems(item);
            }
            else
            {
                if (Items.ContainsKey(item))
                {
                    Items[item] = quantity;
                }
                else
                {
                    Items.Add(item, quantity);
                }
            }
            return true;
        }

        public void Checkout(IInventoryDataAccess inventoryDataAccess)
        {
            foreach(KeyValuePair<Item, int> itemAmount in Items)
            {
                ItemDTO itemDTO = inventoryDataAccess.GetItem(itemAmount.Key.UPC);

                inventoryDataAccess.ChangeQuantity(itemDTO.UPC, itemDTO.Quantity - itemAmount.Value);
            }
        }
    }
}

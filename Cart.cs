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
            int currentCount = 0;
            Items.TryGetValue(item, out currentCount);
            Items.Add(item, currentCount + 1);
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

        //public bool RemoveAllItems(Item item)
        //{
        //    if()
        //}
    }
}

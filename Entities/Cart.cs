using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    class Cart
    {
        //public Dictionary<Item, int> Items { get; set; }
        public List<DTOs.SalesItemDTO> Items;
        //public class SalesItem
        //{
        //    public ItemDTO Item { get; set; }
        //    public int Quantity { get; set; }

        //    public override bool Equals(object obj)
        //    {
        //        return obj is SalesItem && ((SalesItem)obj).Item.UPC.Equals(Item.UPC);
        //    }

        //    public override int GetHashCode()
        //    {
        //        return base.GetHashCode();
        //    }
        //}

        public Cart()
        {
            //Items = new Dictionary<Item, int>();
            Items = new List<DTOs.SalesItemDTO>();
        }
        /// <summary>
        /// The total cost of all Items in the Cart
        /// </summary>
        public decimal TotalCost 
        {
            get
            {
                //return Items.Select(x => x.Key.Price * x.Value).Sum();
                decimal total = Items.Select(x => x.Item.Price * x.Quantity).Sum();
                return total += total * BusinessInfo.TaxAmount;
            }
        }

        /// <summary>
        /// Adds an item to the cart
        /// </summary>
        /// <param name="item">The item to be added</param>
        public void AddItem(ItemDTO item)
        {
            /* //ORIGINAL
            int currentCount = 0;
            Items.TryGetValue(item, out currentCount);
            Items.Add(item, currentCount + 1); // ORIGINAL*/

            //TEST
            //int currentCount = 0;
            //bool result = Items.TryGetValue(item, out currentCount);
            //if (result)
            //{
            //    Items[item] = currentCount + 1;
            //}
            //else
            //{
            //    Items.Add(item, 1);
            //}
            //TEST
            DTOs.SalesItemDTO salesItem;
            if ((salesItem = Items.Find(x => x.Item.Equals(item))) != null){
                salesItem.Quantity++;
            }
            else
            {
                Items.Add(new DTOs.SalesItemDTO
                {
                    Item = item,
                    Quantity = 1
                });
            }
        }

        /// <summary>
        /// Removes one of the given item from the cart
        /// </summary>
        /// <param name="item">The item to be removed</param>
        /// <returns>True if an item was remvoed otherwise false</returns>
        public bool RemoveSingleItem(ItemDTO item)
        {
            bool itemRemoved = false;
            //int count;
            //if(Items.TryGetValue(item, out count))
            //{
            //    if(count > 1)
            //    {
            //        //Items.Add(item, count - 1);
            //        Items.Remove(item);
            //        Items.Add(item, count - 1);
            //    }
            //    else
            //    {
            //        Items.Remove(item);
            //    }
            //    itemRemoved = true;
            //}
            DTOs.SalesItemDTO salesItem;
            if((salesItem = Items.Find(x => x.Item.Equals(item))) != null)
            {
                itemRemoved = true;
                if(salesItem.Quantity > 1)
                {
                    salesItem.Quantity--;
                }
                else
                {
                    Items.Remove(salesItem);
                }
            }
                return itemRemoved;
        }

        /// <summary>
        /// Removes all of a given item from the cart
        /// </summary>
        /// <param name="item">The item to remove all of</param>
        /// <returns>True if items were removed, otherwise false</returns>
        public bool RemoveAllItems(ItemDTO item)
        {
            //bool itemsRemoved = false;
            //if (Items.ContainsKey(item))
            //{
            //    Items.Remove(item);
            //    itemsRemoved = true;
            //}
            return Items.Remove(Items.Find(x => x.Item.Equals(item)));
        }

        public bool ChangeQuantity(int quantity, ItemDTO item)
        {
            if (quantity == 0)
            {
                RemoveAllItems(item);
            }
            else
            {
                DTOs.SalesItemDTO salesItem;
                if((salesItem = Items.Find(x => x.Item.Equals(item))) != null)
                {
                    salesItem.Quantity = quantity;
                }
                else
                {
                    Items.Add(new DTOs.SalesItemDTO
                    {
                        Item = item,
                        Quantity = quantity
                    });
                }
            //    if (Items.ContainsKey(item))
            //    {
            //        Items[item] = quantity;
            //    }
            //    else
            //    {
            //        Items.Add(item, quantity);
            //    }
            }
            return true;
        }

        public void Checkout(IInventoryDataAccess inventoryDataAccess)
        {
            //foreach(KeyValuePair<Item, int> itemAmount in Items)
            //{
            //    ItemDTO itemDTO = inventoryDataAccess.GetItem(itemAmount.Key.UPC);

            //    inventoryDataAccess.ChangeQuantity(itemDTO.UPC, itemDTO.Quantity - itemAmount.Value);
            //}
            foreach(DTOs.SalesItemDTO si in Items)
            {
                ItemDTO itemDTO = inventoryDataAccess.GetItem(si.Item.UPC);
                inventoryDataAccess.ChangeQuantity(itemDTO.UPC, itemDTO.Quantity - si.Quantity);
            }
        }

        public void ReturnItems(IInventoryDataAccess inventoryDataAccess)
        {
            foreach (DTOs.SalesItemDTO si in Items)
            {
                ItemDTO itemDTO = inventoryDataAccess.GetItem(si.Item.UPC);
                inventoryDataAccess.ChangeQuantity(itemDTO.UPC, itemDTO.Quantity + si.Quantity);
            }
        }
    }
}

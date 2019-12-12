using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    /// <summary>
    /// The class representing the cart of items 
    /// </summary>
    class Cart
    {
        public List<DTOs.SalesItemDTO> Items;

        public Cart()
        {
            Items = new List<DTOs.SalesItemDTO>();
        }

        /// <summary>
        /// The total cost of all Items in the Cart
        /// </summary>
        public decimal TotalCost 
        {
            get
            {
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
            }
            return true;
        }

        /// <summary>
        /// Checks out the items currently in the cart
        /// </summary>
        /// <param name="inventoryDataAccess">The inventory data access to use to chekcout the items</param>
        public void Checkout(IInventoryDataAccess inventoryDataAccess)
        {
            foreach(DTOs.SalesItemDTO si in Items)
            {
                ItemDTO itemDTO = inventoryDataAccess.GetItem(si.Item.UPC);
                inventoryDataAccess.ChangeQuantity(itemDTO.UPC, itemDTO.Quantity - si.Quantity);
            }
        }

        /// <summary>
        /// Returns the items in the cart
        /// </summary>
        /// <param name="inventoryDataAccess">The inventory Data Access to use to checkout the items</param>
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

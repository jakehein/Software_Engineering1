using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    class InventoryController : IInventoryController
    {
        /**
         * private global iDA with constructor
         */
        private readonly IInventoryDataAccess iDA;
        public InventoryController(IInventoryDataAccess iDA)
        {
            this.iDA = iDA;
        }

        /**
         * This method checks to see if an item is valid for insertion into the iDA. If so,
         * the item is stored into iDA via a call to CreateItem();
         * @param item object being created in iDA
         * @return boolean detailing if operation was carried out
         */
        public bool CreateItem(ItemDTO item)
        {
            Item newItem = Item.createItemFromDTO(item);

            if (GetItem(item.UPC) == null && newItem.DataWarnings.Count == 0)
            {
                return iDA.CreateItem(item);
            }
            else
            {
                return false;
            }
        }
        
        /**
         * This method checks to see if an item is valid for deletion from the iDA. If so,
         * the item is deleted from iDA via a call to DeleteItem();
         * @param UPC string value being checked for before carrying out deletion
         * @return boolean detailing if operation was carried out
         */
        public bool DeleteItem(string uPC)
        {
            return iDA.DeleteItem(uPC);
        }
        
        /**
         * This method creates a list of UPCs for the calling method to act on. 
         * @return List<string> of all UPC values contained in iDA
         */
        public List<string> AllUPCs
        {
            get
            {
                return iDA.GetAllUPCs();
            }
        }

        /**
        * This method creates a list of all Item objects contained in iDA
        * @return List<Item> of all Item objects contained in iDA
        */
        public List<ItemDTO> GetAllItems()
        {
            return iDA.GetAllItems();
        }
        
        /**
         * This method searches iDA for an Item object with a given UPC string value
         * @param UPC value being searched for in iDA
         * @return Item object contained in iDA
         */
        public ItemDTO GetItem(string uPC)
        {
            return iDA.GetItem(uPC);
        }

        /**
         * This method checks to see if a pLU and item are valid input for update into iDA. If so,
         * the item is updated into iDA via a call to UpdateItem();
         * @param UPC of item to be updated
         * @param item object being updated
         * @return boolean detailing if operation was carried out
         */
        public bool UpdateItem(string uPC, ItemDTO itemDTO)
        {
            return iDA.UpdateItem(uPC, itemDTO);
        }

        
        /// <summary>
        /// This method gets all items from a specified category.
        /// </summary>
        /// <param name="iD">iD of the wanted category</param>
        /// <returns>List of ItemDTOs that represents objects within a category</returns>
        public List<ItemDTO> GetAllItemsFromCategory(long iD)
        {
            return iDA.GetAllItemsFromCategory(iD);
        }

        public bool UpdateItemCategory(ItemDTO item, CategoryDTO category)
        {
            return iDA.UpdateItemCategory(item, category);
        }
    }
}

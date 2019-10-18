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
         private IInventoryDataAccess iDA;
         public InventoryController(IInventoryDataAccess iDA)
         {
             this.iDA = iDA;
         }
        //private InventoryDataAccess iDA = new InventoryDataAccess();
        /**
         * This method checks to see if an item is valid for insertion into the iDA. If so,
         * the item is stored into iDA via a call to CreateItem();
         * @param item object being created in iDA
         * @return boolean detailing if operation was carried out
         */
        public bool CreateItem(ItemDTO item)
        {
            //InventoryDataAccess iDA = new IInventoryDataAccess();

            Item newItem = Item.createItemFromDTO(item);

            if (GetItem(item.UPC) == null && newItem.DataWarnings.Count == 0)
            {
                iDA.CreateItem(item);
                return true;
            }
            else
            {
                return false;
            }
            //throw new NotImplementedException();
        }
        /**
         * This method checks to see if an item is valid for deletion from the iDA. If so,
         * the item is deleted from iDA via a call to DeleteItem();
         * @param pLU string value being checked for before carrying out deletion
         * @return boolean detailing if operation was carried out
         */
        public bool DeleteItem(string pLU)
        {
            //InventoryDataAccess iDA = new IInventoryDataAccess();
            if (!AllPLUs.Contains(pLU))
            {
                iDA.DeleteItem(pLU);
                return true;
            }
            else
            {
                return false;
            }
            //throw new NotImplementedException();
        }
        /**
         * This method creates a list of PLUs for the calling method to act on. 
         * @return List<string> of all PLU values contained in iDA
         */
        public List<string> AllPLUs
        {
            get
            {
                //InventoryDataAccess iDA = new IInventoryDataAccess();
                List<string> allPLUs = new List<string>();
                List<ItemDTO> items = new List<ItemDTO>();
                foreach (ItemDTO item in items)
                {
                    allPLUs.Add(item.UPC);
                }
                return allPLUs;
                //throw new NotImplementedException();
            }
        }

        /**
        * This method creates a list of all Item objects contained in iDA
        * @return List<Item> of all Item objects contained in iDA
        */
        public List<ItemDTO> GetAllItems()
        {
            //InventoryDataAccess iDA = new IInventoryDataAccess();
            return iDA.GetAllItems();

            //throw new NotImplementedException();
        }
        /**
         * This method searches iDA for an Item object with a given pLU string value
         * @param pLU value being searched for in iDA
         * @return Item object contained in iDA
         */
        public ItemDTO GetItem(string uPC)
        {
            return iDA.GetItem(uPC);
            //throw new NotImplementedException();
        }
        /**
         * This method checks to see if a pLU and item are valid input for update into iDA. If so,
         * the item is updated into iDA via a call to UpdateItem();
         * @param pLU of item to be updated
         * @param item object being updated
         * @return boolean detailing if operation was carried out
         */
        public bool UpdateItem(string pLU, ItemDTO itemDTO)
        {
            //InventoryDataAccess iDA = new IInventoryDataAccess();
            List<string> existingUPCs = iDA.GetAllUPCs();

            Item item = Item.createItemFromDTO(itemDTO);

            if (existingUPCs.Contains(pLU) && item.DataWarnings.Count == 0)
            {
                iDA.UpdateItem(pLU, itemDTO);
                return true;
            }
            else
            {
                return false;
            }
            //throw new NotImplementedException();
        }
    }
}

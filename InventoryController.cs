using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    class InventoryController : IInventoryController
    {
        public bool CreateItem(Item item)
        {
            InventoryDataAccess iDA = new IInventoryDataAccess();
            if (GetItem(item.GetPLU) != item.GetPLU)
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

        public bool DeleteItem(string pLU)
        {
            InventoryDataAccess iDA = new IInventoryDataAccess();
            if (!GetAllPLUs().Contains(pLU))
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
        public List<string> GetAllPLUs()
        {
            throw new NotImplementedException();
        }
        public List<Item> GetAllItems()
        {
            throw new NotImplementedException();
        }

        public Item GetItem(string iD)
        {
            throw new NotImplementedException();
        }

        public bool UpdateItem(string iD, Item item)
        {
            throw new NotImplementedException();
        }
    }
}

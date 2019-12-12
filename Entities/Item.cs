using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    class Item
    {
        public long ItemID { get; set; }
        public string UPC { get; set; } = "999999999999";

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (value.Length > 50)
                {
                    _name = value.Substring(0, 50);
                    DataWarnings.Add("Name cannot be greater than 50 characters");
                }
                else _name = value;
            } 
        }
        public int Quantity { get; set; }

        private decimal _price = 0.0m;
        public decimal Price
        {
            get => _price;
            set
            {
                if (value < 0)
                {
                    _price = 0.0m;
                    DataWarnings.Add("Price cannot be less than 0");
                }
                else
                {
                    _price = value;
                }
            }
        }

        public Category Category { get; set; }

        /// <summary>
        /// Holds any data warnings created when this object's attributes were assinged values
        /// </summary>
        public List<string> DataWarnings { get; set; }


        /// <summary>
        /// Creates an Item from the ItemDTO
        /// </summary>
        /// <param name="dto">The DTO to be converted</param>
        /// <returns>The created Item</returns>
        public static Item createItemFromDTO(ItemDTO dto)
        {
            Item item = new Item();
            item.DataWarnings = new List<string>();
            item.ItemID = dto.ItemID;
            item.UPC = dto.UPC;
            item.Name = dto.Name;
            item.Quantity = dto.Quantity;
            item.Price = dto.Price;
            item.Category = dto.Category;
            return item;
        }
        
        /// <summary>
        /// Creates an Item DTO from the Item
        /// </summary>
        /// <param name="item">The item to be converted</param>
        /// <returns>The created ItemDTO</returns>
        public static ItemDTO createDTOfromItem(Item item)
        {
            ItemDTO dto = new ItemDTO();
            dto.ItemID = item.ItemID;
            dto.UPC = item.UPC;
            dto.Name = item.Name;
            dto.Quantity = item.Quantity;
            dto.Price = item.Price;
            dto.Category = item.Category;
            return dto;
        }

        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }
            if (!(obj is Item))
            {
                return false;
            }
            return this.UPC == (obj as Item).UPC;
        }

        public override int GetHashCode()
        {
            return UPC.GetHashCode();
        }
    }
}

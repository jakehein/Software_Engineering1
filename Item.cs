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
        public string UPC { get; set; }

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
        public int? Quantity { get; set; }

        private decimal? _price = null;
        public decimal? Price
        {
            get => _price;
            set
            {
                if (value < 0)
                {
                    _price = 0.0m;
                    DataWarnings.Add("Price cannot be less than 0");
                }
                else;
                {
                    _price = value;
                }
            }
        }
        public Category Category { get; set; }
        public List<string> DataWarnings { get; set; }

        public static Item createItemFromDTO(ItemDTO dto)
        {
            Item item = new Item();
            item.ItemID = dto.ItemID;
            item.UPC = dto.UPC;
            item.Name = dto.Name;
            item.Quantity = dto.Quantity;
            item.Price = dto.Price;
            item.Category = dto.Category;
            return item;
        }
        
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
    }
}

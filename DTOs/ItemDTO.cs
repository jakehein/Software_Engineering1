using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    /// <summary>
    /// A DTO representing the Item class
    /// </summary>
    class ItemDTO
    {
        public long ItemID { get; set; }
        public string UPC { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public long CategoryID { get; set; }

        override
        public string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return obj is ItemDTO && ((ItemDTO)obj).UPC.Equals(UPC);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

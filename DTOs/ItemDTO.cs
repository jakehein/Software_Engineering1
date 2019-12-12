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

        /// <summary>
        /// This method overrides the standard ToString() returning a name
        /// </summary>
        /// <returns>Name of category</returns>
        override
        public string ToString()
        {
            return Name;
        }

        /// <summary>
        /// This method determines if the objects are equal as well as if it is a ItemDTO or not
        /// </summary>
        /// <param name="obj">object we are comparing</param>
        /// <returns>boolean value of whether the upc equals </returns>
        public override bool Equals(object obj)
        {
            return obj is ItemDTO && ((ItemDTO)obj).UPC.Equals(UPC);
        }

        /// <summary>
        /// This method gets the hashcode of the base. Overrides the original method.
        /// </summary>
        /// <returns>int hash code of the base</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

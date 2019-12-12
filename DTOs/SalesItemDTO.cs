using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1.DTOs
{
    class SalesItemDTO
    {
        public ItemDTO Item { get; set; }
        public int Quantity { get; set; }

        /// <summary>
        /// This method determines if the UPC is the same as the object. This also checks for that it is a SalesItemDTO
        /// </summary>
        /// <param name="obj">A SalesItemDTO object we are comparing to</param>
        /// <returns>true if a salesItemDTO and UPC's equal</returns>
        public override bool Equals(object obj)
        {
            return obj is SalesItemDTO && ((SalesItemDTO)obj).Item.UPC.Equals(Item.UPC);
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

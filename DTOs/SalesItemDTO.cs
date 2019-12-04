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

        public override bool Equals(object obj)
        {
            return obj is SalesItemDTO && ((SalesItemDTO)obj).Item.UPC.Equals(Item.UPC);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

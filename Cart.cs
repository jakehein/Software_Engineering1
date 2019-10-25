using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    class Cart
    {
        public List<Item> Items { get; set; }

        public decimal TotalCost 
        {
            get
            {
                return Items.Select(x => x.Price).Sum();
            }
        }
    }
}

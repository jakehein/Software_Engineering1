using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    class CategoryDTO
    {
        public long CategoryID { get; set; }
        public string Name { get; set; }
        public List<Item> Items { get; set; }
    }
}

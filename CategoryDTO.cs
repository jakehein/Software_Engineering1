using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    /// <summary>
    /// Data transfer object for the Category class
    /// </summary>
    class CategoryDTO
    {
        public long CategoryID { get; set; }
        public string Name { get; set; }
        public List<ItemDTO> Items { get; set; }
        
        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(Object obj)
        {
            CategoryDTO c = obj as CategoryDTO;
            return c != null && c.CategoryID == CategoryID;
        }

        public override int GetHashCode()
        {
            return CategoryID.ToString().GetHashCode();
        }
    }
}

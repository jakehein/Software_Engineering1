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
    class CategoryDTO: IComparable
    {
        public long CategoryID { get; set; }
        public string Name { get; set; }
        public List<ItemDTO> Items { get; set; }
        
        /// <summary>
        /// Overrides to string to return the category name
        /// </summary>
        /// <returns> String name of category</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// This equals the 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(Object obj)
        {
            return obj is CategoryDTO c && c.CategoryID == CategoryID;
        }

        /// <summary>
        /// This method compares a category object to another object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>int category difference </returns>
        public int CompareTo(Object obj)
        {
            CategoryDTO c = obj as CategoryDTO;
            return (int)(CategoryID - c.CategoryID);
        }

        /// <summary>
        /// This method gets the hash code of the category ID
        /// </summary>
        /// <returns>int hash code</returns>
        public override int GetHashCode()
        {
            return CategoryID.ToString().GetHashCode();
        }
    }
}

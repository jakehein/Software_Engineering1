using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    /// <summary>
    /// The class represnting a Category of items in the inventory system
    /// </summary>
    class Category
    {
        public long CategoryID { get; set; }
        public string Name { get; set; }
        public List<Item> Items { get; set; }
        public List<int> ItemIDs { get; set; }

        /// <summary>
        /// Takes the categoryDTO item and creates a category object from it which is then returned.
        /// </summary>
        /// <param name="dto"> CategoryDTO being evaluated in transition to Category</param>
        /// <returns>Category variable from the categoryDTO</returns>
        public static Category createCategoryFromDTO(CategoryDTO dto)
        {
            Category category = new Category();
            category.CategoryID = dto.CategoryID;
            category.Name = dto.Name;

            if(dto.Items != null)
            {
                foreach(ItemDTO itemDTO in dto.Items)
                {
                    category.Items.Add(Item.createItemFromDTO(itemDTO));
                }
            }

            return category;
        }

        /// <summary>
        /// Creates a categoryDTO when passed in a category. Each item present in the category is placed within
        /// the categoryDTO
        /// </summary>
        /// <param name="category"> category to converted into CategoryDTO</param>
        /// <returns> categoryDTO of the cateogry</returns>
        public static CategoryDTO createDTOFromCategory(Category category)
        {
            CategoryDTO categoryDTO = new CategoryDTO();
            categoryDTO.CategoryID = category.CategoryID;
            categoryDTO.Name = category.Name;

            if(category.Items != null)
            {
                foreach(Item item in category.Items)
                {
                    categoryDTO.Items.Add(Item.createDTOfromItem(item));
                }
            }
            return categoryDTO;
        }
    }
}

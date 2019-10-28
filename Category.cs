using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    class Category
    {
        public long CategoryID { get; set; }
        public string Name { get; set; }
        public List<Item> Items { get; set; }
        public List<int> ItemIDs { get; set; }

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

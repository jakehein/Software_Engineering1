using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    interface ICategoryController
    {
        bool CreateCategory(CategoryDTO category);

        bool UpdateCategory(long iD, string category);

        bool DeleteCategory(CategoryDTO category);

        CategoryDTO GetCategory(long categoryID);

        List<CategoryDTO> GetAllCategories();

        List<CategoryDTO> GetAllCategoriesWithItems();

        CategoryDTO GetCategoryByName(string categoryName);
    }
}

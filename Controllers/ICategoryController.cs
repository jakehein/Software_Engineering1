using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    interface ICategoryController
    {
        /// <summary>
        /// Add the passed in Category to the data source if valid
        /// </summary>
        /// <param name="category">Category to add</param>
        /// <returns>True if Category added</returns>
        bool CreateCategory(CategoryDTO category);

        /// <summary>
        /// Update the passed in Category to the data source is valid
        /// </summary>
        /// <param name="category">Name of category to be changed to // name of the new category</param>
        /// <param name="iD"> iD of the original category </param>
        /// <returns> true if the category is successfully updated</returns>
        bool UpdateCategory(long iD, string category);

        /// <summary>
        /// Delete the passed in Category within the data source
        /// </summary>
        /// <param name="category"> CategoryDTO of category to be deleted</param>
        /// <returns> true if category was successfully deleted</returns>
        bool DeleteCategory(CategoryDTO category);

        /// <summary>
        /// Get the Category that corresponds with the passed in id 
        /// </summary>
        /// <param name="categoryID">Category ID</param>
        /// <returns>Category associated with id</returns>
        CategoryDTO GetCategory(long categoryID);

        /// <summary>
        /// Get all categories from the datasource
        /// </summary>
        /// <returns>List of all Category</returns>
        List<CategoryDTO> GetAllCategories();

        /// <summary>
        /// Gets a List of CategoryDTOS that have items
        /// </summary>
        /// <returns> List of Category DTOs containing items</returns>
        List<CategoryDTO> GetAllCategoriesWithItems();

        /// <summary>
        /// Retrives the Category Data Transfer Object of a category by looking it up by its name.
        /// </summary>
        /// <param name="categoryName"> string name of the category</param>
        /// <returns> CategoryDTO of the category specified by categoryName </returns>
        CategoryDTO GetCategoryByName(string categoryName);
    }
}

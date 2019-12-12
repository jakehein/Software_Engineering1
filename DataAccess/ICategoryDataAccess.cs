using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    interface ICategoryDataAccess
    {
        /// <summary>
        /// Add a Category to the database if it doesn't already exist
        /// </summary>
        /// <param name="category">Category to add</param>
        /// <returns>True if Category added</returns>
        bool CreateCategory(CategoryDTO category);

        /// <summary>
        /// This method updates the category with a new title specified
        /// </summary>
        /// <param name="iD"> iD of the original category</param>
        /// <param name="category">Category to Update To // name of new category</param>
        /// <returns> true if the category is successfully updated</returns>
        bool UpdateCategory(long iD, string category);

        /// <summary>
        /// This method allows for the deletion of a category. The items within the deleted category will 
        /// be recatagorized as unclassified.
        /// </summary>
        /// <param name="category"> is the category to be deleted</param>
        /// <returns> true if the category is successfully deleted.</returns>
        bool DeleteCategory(CategoryDTO category);

        /// <summary>
        /// Get a Category that corresponds with passed in id
        /// </summary>
        /// <param name="categoryID">ID of the Category</param>
        /// <returns>Category associated with id</returns>
        CategoryDTO GetCategory(long categoryID);

        /// <summary>
        /// Get a category that corresponds with the provided name
        /// </summary>
        /// <param name="categoryName"> name of the category we wish to get the DTO from</param>
        /// <returns> CategoryDTO of the category that corresponds to the name</returns>
        CategoryDTO GetCategoryByName(string categoryName);

        /// <summary>
        /// Get all of the Categories in the database
        /// </summary>
        /// <returns>List containing all Categories</returns>
        List<CategoryDTO> GetCategories();
    }
}

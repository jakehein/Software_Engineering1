using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    class CategoryController : ICategoryController
    {
        private const int NameLength = 50;
        private ICategoryDataAccess categoryDataAccess = DataAccessContainer.Instance.CategoryDataAccess;

        /// <summary>
        /// Add the passed in Category to the data source if valid
        /// </summary>
        /// <param name="category">Category to add</param>
        /// <returns>True if Category added</returns>
        public bool CreateCategory(CategoryDTO category)
        {
            return ValidateName(category.Name) && categoryDataAccess.CreateCategory(category);
        }

        /// <summary>
        /// Update the passed in Category to the data source is valid
        /// </summary>
        /// <param name="category">Name of category to be changed to // name of the new category</param>
        /// <param name="iD"> iD of the original category </param>
        /// <returns> true if the category is successfully updated</returns>
        public bool UpdateCategory(long iD, string category)
        {
            return ValidateName(category) && categoryDataAccess.UpdateCategory(iD, category);
        }

        /// <summary>
        /// Delete the passed in Category within the data source
        /// </summary>
        /// <param name="category"> CategoryDTO of category to be deleted</param>
        /// <returns> true if category was successfully deleted</returns>
        public bool DeleteCategory(CategoryDTO category)
        {
            return categoryDataAccess.DeleteCategory(category);
        }

        /// <summary>
        /// Get the Category that corresponds with the passed in id 
        /// </summary>
        /// <param name="categoryID">Category ID</param>
        /// <returns>Category associated with id</returns>
        public CategoryDTO GetCategory(long categoryID)
        {
            return categoryDataAccess.GetCategory(categoryID);
        }

        /// <summary>
        /// Get all categories from the datasource
        /// </summary>
        /// <returns>List of all Category</returns>
        public List<CategoryDTO> GetAllCategories()
        {
            return categoryDataAccess.GetCategories();
        }

        /// <summary>
        /// Gets a List of CategoryDTOS that have items
        /// </summary>
        /// <returns> List of Category DTOs containing items</returns>
        public List<CategoryDTO> GetAllCategoriesWithItems()
        {
            List<CategoryDTO> categoryDTOs = new List<CategoryDTO>();
            return categoryDTOs;
        }

        /// <summary>
        /// Retrives the Category Data Transfer Object of a category by looking it up by its name.
        /// </summary>
        /// <param name="categoryName"> string name of the category</param>
        /// <returns> CategoryDTO of the category specified by categoryName </returns>
        public CategoryDTO GetCategoryByName(string categoryName)
        {
            return categoryDataAccess.GetCategoryByName(categoryName);
        }

        /// <summary>
        /// Test if the Category to be added has a valid name
        /// </summary>
        /// <param name="name">Category name</param>
        /// <returns>True if name is valid</returns>
        private bool ValidateName(string name)
        {
            return name.Length <= NameLength;
        }
    }
}

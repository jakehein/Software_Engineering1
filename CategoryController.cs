﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    class CategoryController
    {
        private const int NameLength = 50;
        private ICategoryDataAccess categoryDataAccess = null;
        public CategoryController(ICategoryDataAccess categoryDataAccess)
        {
            this.categoryDataAccess = categoryDataAccess;
        }

        /// <summary>
        /// Add the passed in Category to the data source if valid
        /// </summary>
        /// <param name="category">Category to add</param>
        /// <returns>True if Category added</returns>
        public bool CreateCategory(CategoryDTO category)
        {

            return ValidateName(category.Name) &&
                   categoryDataAccess.CreateCategory(category);
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

        public List<CategoryDTO> GetAllCategoriesWithItems()
        {
            List<CategoryDTO> categoryDTOs = new List<CategoryDTO>();
            return categoryDTOs;
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

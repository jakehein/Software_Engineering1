using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    class CategoryDataAccess : SQLDB, ICategoryDataAccess
    {
        // Table identifiers
        private const string CategoryTableName = "Category";

        // Column identifiers
        private const string CategoryIDColumn = "CategoryID",
                             CategoryNameColumn = "Name";

        /// <summary>
        /// Check if CategoryExists
        /// </summary>
        /// <param name="categoryID">Category id to search for</param>
        /// <returns>True if Category exists</returns>
        private bool CategoryExists(long categoryID)
        {
            string commandString = "SELECT EXISTS(SELECT 1 FROM " + CategoryTableName +
                                    " WHERE " + CategoryIDColumn + " = '" + categoryID + "' LIMIT 1)";
            return int.Parse(ExecuteScalar(commandString).ToString()) == 0;
        }

        /// <summary>
        /// Add a Category to the database if it doesn't already exist
        /// </summary>
        /// <param name="category">Category to add</param>
        /// <returns>True if Category added</returns>
        public bool CreateCategory(CategoryDTO category)
        {
            int result = -1;
            if (!CategoryExists(category.CategoryID))
            {
                string commandString = "INSERT INTO " + CategoryTableName +
                                        "(" + CategoryIDColumn + ", " +
                                        "(" + CategoryNameColumn +  ")" +
                                        " VALUES(" +
                                        category.CategoryID + ", '" +
                                        category.Name + "')";
                result = ExecuteNonQuery(commandString);
            }
            return result > 0;
        }

        /// <summary>
        /// Get a Category that corrisponds with passed in id
        /// </summary>
        /// <param name="categoryID">ID of the Category</param>
        /// <returns>Category associated with id</returns>
        public CategoryDTO GetCategory(long categoryID)
        {
            CategoryDTO category = null;
            string commandString = "SELECT * FROM " + CategoryTableName + 
                                   " WHERE " + CategoryIDColumn + " = " + categoryID + 
                                   " LIMIT 1";
            ExecuteReader(commandString);
            while (Reader.Read())
            {
                category = ReadCategory();
            }
            CleanUp();
            return category;
        }

        /// <summary>
        /// Get all of the Categories in the database
        /// </summary>
        /// <returns>List containing all Categories</returns>
        public List<CategoryDTO> GetCategories()
        {
            List<CategoryDTO> categories = new List<CategoryDTO>();
            string commandString = "SELECT * FROM " + CategoryTableName;
            ExecuteReader(commandString);
            while (Reader.Read())
            {
                categories.Add(ReadCategory());
            }
            CleanUp();
            return categories;
        }

        /// <summary>
        /// Read in a Category from the Reader
        /// </summary>
        /// <returns>Read in Category</returns>
        private CategoryDTO ReadCategory()
        {
            return new CategoryDTO
            {
                CategoryID = (long)Reader.GetDecimal(CategoryIDColumn),
                Name = Reader.GetString(CategoryNameColumn)
            };
        }
    }
}

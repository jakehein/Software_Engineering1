using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    class CategoryDataAccess : ICategoryDataAccess
    {
        // Table identifiers
        private const string CategoryTableName = "Category";

        // Column identifiers
        private const string CategoryIDColumn = "CategoryID",
                             CategoryNameColumn = "Name";

        private string connectionStringToDB = ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString;

        /// <summary>
        /// Check if CategoryExists
        /// </summary>
        /// <param name="categoryID">Category id to search for</param>
        /// <returns>True if Category exists</returns>
        private bool CategoryExists(long categoryID)
        {
            int result = -1;
            string commandString = $@"SELECT EXISTS(SELECT 1 FROM {CategoryTableName}
                                      WHERE {CategoryIDColumn} = @CategoryID 
                                      LIMIT 1)";
            using (MySqlConnection conn = new MySqlConnection(connectionStringToDB))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(commandString, conn);
                cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                //result = int.Parse(cmd.ExecuteScalar().ToString());
                result = int.Parse(cmd.ExecuteNonQuery().ToString());
            }
            return result > 0;
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
                string commandString = $@"INSERT INTO {CategoryTableName}({CategoryIDColumn}, {CategoryNameColumn})
                                        VALUES(@CategoryID, @CategoryName)";
                using (MySqlConnection conn = new MySqlConnection(connectionStringToDB))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(commandString, conn);
                    cmd.Parameters.AddWithValue("@CategoryID", category.CategoryID);
                    cmd.Parameters.AddWithValue("@CategoryName", category.Name);
                    //result = int.Parse(cmd.ExecuteScalar().ToString());
                    result = int.Parse(cmd.ExecuteNonQuery().ToString());
                    cmd.Dispose();
                }
            }
            return result > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iD"></param>
        /// <param name="category">Category to Update To</param>
        /// <returns></returns>
        public bool UpdateCategory(long iD, string category)
        {
            int result = -1;

            string commandString = $@"UPDATE {CategoryTableName}
                                   SET {CategoryIDColumn} = @CategoryID, {CategoryNameColumn} = @Name
                                   WHERE {CategoryIDColumn} = @CategoryID";
            using (MySqlConnection conn = new MySqlConnection(connectionStringToDB))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(commandString, conn);
                cmd.Parameters.AddWithValue("@Name", category);
                cmd.Parameters.AddWithValue("@CategoryID", iD);
                result = int.Parse(cmd.ExecuteNonQuery().ToString());
                cmd.Dispose();
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
            string commandString = $@"SELECT * FROM {CategoryTableName}
                                      WHERE {CategoryIDColumn} = @CategoryID 
                                      LIMIT 1";
            using (MySqlConnection conn = new MySqlConnection(connectionStringToDB))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(commandString, conn);
                cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    category = ReadCategory(reader);
                }
                cmd.Dispose();
            }

            return category;
        }

        /// <summary>
        /// Get all of the Categories in the database
        /// </summary>
        /// <returns>List containing all Categories</returns>
        public List<CategoryDTO> GetCategories()
        {
            List<CategoryDTO> categories = new List<CategoryDTO>();
            string commandString = $@"SELECT *
                                      FROM {CategoryTableName}";
            using (MySqlConnection conn = new MySqlConnection(connectionStringToDB))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(commandString, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    categories.Add(ReadCategory(reader));
                }
                cmd.Dispose();
            }
            return categories;
        }

        /// <summary>
        /// Read in a Category from the Reader
        /// </summary>
        /// <returns>Read in Category</returns>
        private CategoryDTO ReadCategory(MySqlDataReader reader)
        {
            return new CategoryDTO
            {
                CategoryID = (long)reader.GetDecimal(CategoryIDColumn),
                Name = reader.GetString(CategoryNameColumn)
            };
        }
    }
}

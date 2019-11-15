using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace FinalProject1
{
    class InventoryDataAccess : IInventoryDataAccess
    {
        // Table identifiers
        private const string ItemTableName = "Item";

        // Column identifiers
        private const string CategoryColumn = "CategoryID",
                             ItemIDColumn = "ItemID",
                             NameColumn = "Name",
                             PriceColumn = "Price",
                             QuantityColumn = "Quantity",
                             UPCColumn = "UPC";

        // database connection string
        private string connectionStringToDB = ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString;

        private ICategoryDataAccess categoryDataAccess = null;
        public InventoryDataAccess(ICategoryDataAccess categoryDataAccess)
        {
            this.categoryDataAccess = categoryDataAccess;
        }

        /// <summary>
        /// Check if an Item with the passed in UPC already exists
        /// </summary>
        /// <param name="upc">UPC to check</param>
        /// <returns>True if UPC Exists in database</returns>
        private bool DoesUPCExist(string uPC)
        {
            int result = -1;
            string commandString = $@"SELECT EXISTS(SELECT 1 
                                      FROM { ItemTableName} 
                                      WHERE {UPCColumn} = @UPC 
                                      LIMIT 1)";
            using (MySqlConnection conn = new MySqlConnection(connectionStringToDB))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(commandString, conn);
                cmd.Parameters.AddWithValue("@UPC", uPC);
                result = int.Parse(cmd.ExecuteScalar().ToString());
            }
            return result > 0;
        }

        /// <summary>
        /// Add a new Item to the database if it doesn't already exist
        /// </summary>
        /// <param name="item">Item to be added</param>
        /// <returns>True if Item added successfully</returns>
        public bool CreateItem(ItemDTO item)
        {
            int result = -1;
            if (!DoesUPCExist(item.UPC))
            {
                string commandString = $@"INSERT INTO {ItemTableName} ({CategoryColumn}, {NameColumn}, {PriceColumn}, {QuantityColumn}, {UPCColumn})
                                       VALUES(@CategoryID, @Name, @Price, @Quantity, @UPC)";
 
                using (MySqlConnection conn = new MySqlConnection(connectionStringToDB))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(commandString, conn);
                    cmd.Parameters.AddWithValue("@CategoryId", item.Category.CategoryID);
                    cmd.Parameters.AddWithValue("@Name", item.Name);
                    cmd.Parameters.AddWithValue("@Price", item.Price);
                    cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                    cmd.Parameters.AddWithValue("@UPC", item.UPC);
                    result = int.Parse(cmd.ExecuteNonQuery().ToString());
                    cmd.Dispose();
                }
            }
            return result > 0;
        }

        /// <summary>
        /// Update the values of a passed in Item
        /// </summary>
        /// <param name="uPC">Original UPC of item</param>
        /// <param name="item">Item with new values</param>
        /// <returns>True if Item updated</returns>
        public bool UpdateItem(string uPC, ItemDTO item)
        {
            int result = -1;
            if (uPC.Equals(item.UPC) || !DoesUPCExist(item.UPC))
            {
                string commandString = $@"UPDATE {ItemTableName}
                                   SET {CategoryColumn} = @CategoryID, {NameColumn} = @Name, {PriceColumn} = @Price, {QuantityColumn} = @Quantity, {UPCColumn} = @UPC
                                   WHERE {UPCColumn} = @OriginalUPC";
                using (MySqlConnection conn = new MySqlConnection(connectionStringToDB))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(commandString, conn);
                    cmd.Parameters.AddWithValue("@CategoryId", item.Category.CategoryID);
                    cmd.Parameters.AddWithValue("@Name", item.Name);
                    cmd.Parameters.AddWithValue("@Price", item.Price);
                    cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                    cmd.Parameters.AddWithValue("@UPC", item.UPC);
                    cmd.Parameters.AddWithValue("@ItemID", item.ItemID);
                    cmd.Parameters.AddWithValue("@OriginalUPC", uPC);
                    result = int.Parse(cmd.ExecuteNonQuery().ToString());
                    cmd.Dispose();
                }
            }
            return result > 0;
        }

        /// <summary>
        /// Delete an Item with the passed in UPC from the database
        /// </summary>
        /// <param name="uPC">UPC of Item to be deleted</param>
        /// <returns>True if Item deleted</returns>
        public bool DeleteItem(string uPC)
        {
            int result = -1;
            string commandString = $@"DELETE FROM {ItemTableName}
                                   WHERE {UPCColumn} = @UPC";
            using (MySqlConnection conn = new MySqlConnection(connectionStringToDB))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(commandString, conn);
                cmd.Parameters.AddWithValue("@UPC", uPC);
                result = int.Parse(cmd.ExecuteNonQuery().ToString());
                cmd.Dispose();
            }
            return result > 0;
        }

        /// <summary>
        /// Get Item corresponding with passed in UPC
        /// </summary>
        /// <param name="uPC">Item UPC</param>
        /// <returns>Item associated with UPC</returns>
        public ItemDTO GetItem(string uPC)
        {
            ItemDTO item = null;
            string commandString = $@"SELECT * FROM {ItemTableName}
                                      WHERE {UPCColumn} = @UPC
                                      LIMIT 1";

            using (MySqlConnection conn = new MySqlConnection(connectionStringToDB))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(commandString, conn);
                cmd.Parameters.AddWithValue("@UPC", uPC);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    item = ReadInItem(reader);
                }
                cmd.Dispose();
            }
            return item;
        }

        /// <summary>
        /// Get all Items stored in the database
        /// </summary>
        /// <returns>List containing all Items</returns>
        public List<ItemDTO> GetAllItems()
        {
            List<ItemDTO> items = new List<ItemDTO>();
            string commandString = $@"SELECT * FROM {ItemTableName}
                                      ORDER BY {NameColumn}";

            using (MySqlConnection conn = new MySqlConnection(connectionStringToDB))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(commandString, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    items.Add(ReadInItem(reader));
                }
                cmd.Dispose();
            }
            return items;
        }

        /// <summary>
        /// Get all UPCs stored in the database
        /// </summary>
        /// <returns>List containing all UPCs</returns>
        public List<string> GetAllUPCs()
        {
            List<string> uPCs = new List<string>();
            string commandString = $@"SELECT {UPCColumn}
                                      FROM {ItemTableName}";
            using (MySqlConnection conn = new MySqlConnection(connectionStringToDB))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(commandString, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    uPCs.Add(reader.GetString(UPCColumn));
                }
                cmd.Dispose();
            }
            return uPCs;
        }

        /// <summary>
        /// Updates the quantity of the item with the given UPC
        /// </summary>
        /// <param name="uPC">The UPC of the item to Update</param>
        /// <param name="amount">The new amount to set</param>
        /// <returns>True if the item was updated, otherwise false</returns>
        public bool ChangeQuantity(string uPC, int amount)
        {
            string cmdString = $@"UPDATE {ItemTableName} 
                                    SET {QuantityColumn} = @Amount
                                    WHERE {UPCColumn} = @UPC";
            using (MySqlConnection conn = new MySqlConnection(connectionStringToDB))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(cmdString, conn);
                cmd.Parameters.AddWithValue("@Amount", amount);
                cmd.Parameters.AddWithValue("@UPC", uPC);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            return true;
        }

        /// <summary>
        /// Read in a single Item using the MySQLDataReader
        /// </summary>
        /// <returns>The Item created with the read in values</returns>
        private ItemDTO ReadInItem(MySqlDataReader reader)
        {
            ItemDTO item = new ItemDTO
            {
                ItemID = reader.GetInt64(ItemIDColumn),
                UPC = reader.GetString(UPCColumn),
                Name = reader.GetString(NameColumn),
                Quantity = reader.GetInt32(QuantityColumn),
                Price = (decimal)reader.GetFloat(PriceColumn),
                Category = Category.createCategoryFromDTO(categoryDataAccess.GetCategory((long)reader.GetInt64(CategoryColumn)))
            };
            return item;
        }
        

        /// <summary>
        /// Get all items in a specific category specified by the ID
        /// </summary>
        /// <param name="iD">specified category ID</param>
        /// <returns>List containing all Items with a categoryiD of iD</returns>
        public List<ItemDTO> GetAllItemsFromCategory(long iD)
        {
            //SELECT * FROM `Item` WHERE `CategoryID`
            List<ItemDTO> items = new List<ItemDTO>();
            string commandString = $@"SELECT * FROM {ItemTableName}
                                      WHERE {CategoryColumn} = @CategoryID";

            using (MySqlConnection conn = new MySqlConnection(connectionStringToDB))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(commandString, conn);
                cmd.Parameters.AddWithValue("@CategoryID", iD);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    items.Add(ReadInItem(reader));
                }
                cmd.Dispose();
            }
            return items;
        }

        /// <summary>
        /// This method updates the passed items category to the category specified in 'category' variable
        /// </summary>
        /// <param name="item">item thats category is being updated</param>
        /// <param name="category">is the category that is being updated to</param>
        /// <returns></returns>
        public bool UpdateItemCategory(ItemDTO item, CategoryDTO category)
        {

            int result = -1;
            string commandString = $@"UPDATE {ItemTableName}
                                   SET {CategoryColumn} = @CategoryID
                                   WHERE {UPCColumn} = @UPC";
            using (MySqlConnection conn = new MySqlConnection(connectionStringToDB))
            {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(commandString, conn);
             cmd.Parameters.AddWithValue("@CategoryId", category.CategoryID);
             cmd.Parameters.AddWithValue("@UPC", item.UPC);
             result = int.Parse(cmd.ExecuteNonQuery().ToString());
             cmd.Dispose();
            }
            return result > 0;
        }
    }
}

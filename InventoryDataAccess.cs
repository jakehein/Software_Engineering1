using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace FinalProject1
{
    class InventoryDataAccess : SQLDB, IInventoryDataAccess
    {
        // Table identifiers
        private const string ItemTableName = "Item",
                             CategoryTableName = "Category";

        // Column identifiers
        private const string CategoryColumn = "CategoryID",
                             ItemIDColumn = "ItemID",
                             NameColumn = "Name",
                             PriceColumn = "Price",
                             QuantityColumn = "Quantity",
                             UPCColumn = "UPC",
                             CategoryNameColumn = "Name";

        private ICategoryDataAccess categoryDataAccess = null;

        public InventoryDataAccess() { }
        public InventoryDataAccess(ICategoryDataAccess categoryDataAccess)
        {
            this.categoryDataAccess = categoryDataAccess;
        }
        //private string connectionStringToDB = ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString;
        //private MySqlConnection OpenConnection()
        //{
        //    MySqlConnection conn = new MySqlConnection(connectionStringToDB);
        //    conn.Open();
        //    return conn;
        //}


        /// <summary>
        /// Check if an Item with the passed in UPC already exists
        /// </summary>
        /// <param name="upc">UPC to check</param>
        /// <returns>True if UPC Exists in database</returns>
        private bool DoesUPCExist(string uPC)
        {
            //bool result = false;
            string commandString = "SELECT EXISTS(SELECT 1 FROM " + ItemTableName +
                                    " WHERE " + UPCColumn + " = '" + uPC + "' LIMIT 1)";
            //MySqlConnection conn = OpenConnection();
            //MySqlCommand cmd = new MySqlCommand(commandString, conn);
            //result = int.Parse(cmd.ExecuteScalar().ToString()) == 1;
            //cmd.Dispose();
            //conn.Close();
            return int.Parse(ExecuteScalar(commandString).ToString()) == 0;
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
                //MySqlConnection conn = OpenConnection();

                string commandString = "INSERT INTO " + ItemTableName +
                                        "(" + CategoryColumn + ", " +
                                        "(" + NameColumn + ", " +
                                        "(" + PriceColumn + ", " +
                                        "(" + QuantityColumn + ", " +
                                        "(" + UPCColumn + ")" +
                                        " VALUES(" +
                                        item.Category.CategoryID + ", '" +
                                        item.Name + "', " +
                                        item.Price + ", " +
                                        item.Quantity + ", '" +
                                        item.UPC + "')";
                //MySqlCommand cmd = new MySqlCommand(commandString, conn);
                //result = cmd.ExecuteNonQuery();
                //cmd.Dispose();
                //conn.Close();
                result = ExecuteNonQuery(commandString);
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
                //MySqlConnection conn = OpenConnection();
                string commandString = "UPDATE " + ItemTableName + " SET " + 
                                       CategoryColumn + " = " + item.Category.CategoryID +
                                       ", " + NameColumn  + " = '" + item.Name +
                                       "', " + PriceColumn + " = " + item.Price +
                                       ", " + QuantityColumn + " = " + item.Quantity + 
                                       ", '" + UPCColumn + " = '" + item.UPC +
                                       "' WHERE " + ItemIDColumn + " = " + item.ItemID;
                //MySqlCommand cmd = new MySqlCommand(commandString, conn);
                //cmd.ExecuteNonQuery();
                //cmd.Dispose();
                //conn.Close();
                return ExecuteNonQuery(commandString) > 0;
        }

        /// <summary>
        /// Delete an Item with the passed in UPC from the database
        /// </summary>
        /// <param name="uPC">UPC of Item to be deleted</param>
        /// <returns>True if Item deleted</returns>
        public bool DeleteItem(string uPC)
        {
            //int result = -1;
            //MySqlConnection conn = OpenConnection();
            string commandString = "DELETE FROM " + ItemTableName +
                                   " WHERE " + UPCColumn + " = '" + uPC + "'";
            //MySqlCommand cmd = new MySqlCommand(commandString, conn);
            //result = cmd.ExecuteNonQuery();
            //cmd.Dispose();
            //conn.Close();
            //return result > 0;
            return ExecuteNonQuery(commandString) > 0;
        }

        /// <summary>
        /// Get Item corresponding with passed in UPC
        /// </summary>
        /// <param name="uPC">Item UPC</param>
        /// <returns>Item associated with UPC</returns>
        public ItemDTO GetItem(string uPC)
        {
            ItemDTO item = null;
            //string commandString = "SELECT * FROM " + ItemTableName +
            //                       " INNER JOIN " + CategoryTableName +
            //                       " ON " + ItemTableName + "." + CategoryColumn + 
            //                       " = " + CategoryTableName + "."  + CategoryColumn +
            //                       " WHERE " + UPCColumn + " = '" + uPC + "' LIMIT 1";
            string commandString = "SELECT * FROM " + ItemTableName +
                                   " WHERE " + UPCColumn + " = '" + uPC + "' LIMIT 1";

            //MySqlConnection conn = OpenConnection();
            //MySqlCommand cmd = new MySqlCommand(commandString, conn);
            //MySqlDataReader reader = cmd.ExecuteReader();
            ExecuteReader(commandString);
            while (Reader.Read())
            {
                item = ReadInItem();
            }
            CleanUp();
            //reader.Close();
            //cmd.Dispose();
            //conn.Close();
            return item;
        }

        /// <summary>
        /// Get all Items stored in the database
        /// </summary>
        /// <returns>List containing all Items</returns>
        public List<ItemDTO> GetAllItems()
        {
            List<ItemDTO> items = new List<ItemDTO>();
            //string commandString = "SELECT * FROM " + ItemTableName + 
            //                       " INNER JOIN " + CategoryTableName +
            //                       " ON " + ItemTableName + "." + CategoryColumn +
            //                       " = " + CategoryTableName + "." + CategoryColumn;
            string commandString = "SELECT * FROM " + ItemTableName;

            //MySqlConnection conn = OpenConnection();
            //MySqlCommand cmd = new MySqlCommand(commandString, conn);
            //MySqlDataReader reader = cmd.ExecuteReader();
            ExecuteReader(commandString);
            while (Reader.Read())
            {
                items.Add(ReadInItem());
            }
            CleanUp();
            //reader.Close();
            //cmd.Dispose();
            //conn.Close();
            return items;
        }

        /// <summary>
        /// Get all UPCs stored in the database
        /// </summary>
        /// <returns>List containing all UPCs</returns>
        public List<string> GetAllUPCs()
        {
            List<string> uPCs = new List<string>();
            string commandString = "SELECT " + UPCColumn + " FROM " + ItemTableName;
            //MySqlConnection conn = OpenConnection();
            //MySqlCommand cmd = new MySqlCommand(commandString, conn);
            //MySqlDataReader reader = cmd.ExecuteReader();
            ExecuteReader(commandString);
            while (Reader.Read())
            {
                uPCs.Add(Reader.GetString(UPCColumn));
            }
            return uPCs;
        }

        /// <summary>
        /// Read in a single Item using the MySQLDataReader
        /// </summary>
        /// <returns>The Item created with the read in values</returns>
        private ItemDTO ReadInItem()
        {
            //Category category = new Category
            //{
            //    CategoryID = Reader.GetInt64(CategoryTableName + "." + CategoryColumn),
            //    Name = Reader.GetString(CategoryTableName + "." + CategoryNameColumn),
            //    Items = null
            //};

            ItemDTO item = new ItemDTO
            {
                ItemID = Reader.GetInt64(ItemIDColumn),
                UPC = Reader.GetString(UPCColumn),
                Name = Reader.GetString(NameColumn),
                Quantity = Reader.GetInt32(QuantityColumn),
                Price = (decimal)Reader.GetFloat(PriceColumn),
                //Category = category
                Category = categoryDataAccess.GetCategory((long)Reader.GetDecimal(CategoryColumn))
            };
            return item;
        }
    }
}

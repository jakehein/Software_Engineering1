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
        // Table and Column identifiers
        private const string ItemTableName = "Item",
                             CategoryColumn = "CategoryID",
                             ItemIDColumn = "ItemID",
                             NameColumn = "Name",
                             PriceColumn = "Price",
                             QuantityColumn = "Quantity",
                             UPCColumn = "UPC";

        private string connectionStringToDB = ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString;
        private MySqlConnection OpenConnection()
        {
            MySqlConnection conn = new MySqlConnection(connectionStringToDB);
            conn.Open();
            return conn;
        }

        /*
         * Check if an item with the given upc already exists in the database
         * Returns true if the item exists
         */
        private bool DoesUPCExist(string upc)
        {
            bool result = false;
            string commandString = "SELECT EXISTS(SELECT 1 FROM " + TableName +
                                    " WHERE " + UPCColumn + " = '" + upc + "' LIMIT 1)";
            MySqlConnection conn = OpenConnection();
            MySqlCommand cmd = new MySqlCommand(commandString, conn);
            result = int.Parse(cmd.ExecuteScalar().ToString()) == 1;
            cmd.Dispose();
            conn.Close();
            return result;
        }

        /*
         * Add the passed in Item to the DB if it doesn't already exist
         * Returns true if Item is successfully added
         */
        public bool CreateItem(Item item)
        {
            int result = -1;
            if (!DoesUPCExist(/*item.UPC*/""))
            {
                MySqlConnection conn = OpenConnection();

                string commandString = "INSERT INTO " + TableName +
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
                MySqlCommand cmd = new MySqlCommand(commandString, conn);
                result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
            }
            return result > 0;
        }

        /*
         * Update the DB table values of an Item
         * returns true if the Item is successfully updated
         */
        public bool UpdateItem(string pLU, Item item)
        {
            if (DoesUPCExist(item.UPC))
            {
                MySqlConnection conn = OpenConnection();
                string commandString = "UPDATE " + TableName + " SET " + 
                                       CategoryColumn + " = " + item.Category.CategoryID +
                                       ", " + NameColumn  + " = '" + item.Name +
                                       "', " + PriceColumn + " = " + item.Price +
                                       ", " + QuantityColumn + " = " + item.Quantity + 
                                       ", '" + UPCColumn + " = '" + item.UPC +
                                       "' WHERE " + ItemIDColumn + " = " + item.ItemID;
                MySqlCommand cmd = new MySqlCommand(commandString, conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
                return true;
            }
            return false;
        }

        /*
         * Delete an Item from the DB table if it exists
         * Returns true if the item is removed
         */
        public bool DeleteItem(string pLU)
        {
            int result = -1;
            MySqlConnection conn = OpenConnection();
            string commandString = "DELETE FROM " + TableName +
                                   " WHERE " + UPCColumn + " = '" + pLU + "'";
            MySqlCommand cmd = new MySqlCommand(commandString, conn);
            result = cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            return result > 0;
        }

        /*
         * Returns an Item if it exists otherwise returns null
         */
        public Item GetItem(string pLU)
        {
            Item item = null;
            string commandString = "SELECT * FROM " + TableName +
                                   " WHERE " + UPCColumn + " = '" + pLU + "' LIMIT 1";
            MySqlConnection conn = OpenConnection();
            MySqlCommand cmd = new MySqlCommand(commandString, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                item = ReadInItem(reader);
            }
            reader.Close();
            cmd.Dispose();
            conn.Close();
            return item;
        }

        /*
         * Returns a List containing all Items in the DB
         */
        public List<Item> GetAllItems()
        {
            List<Item> items = new List<Item>();
            string commandString = "SELECT * FROM " + TableName;
            MySqlConnection conn = OpenConnection();
            MySqlCommand cmd = new MySqlCommand(commandString, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                items.Add(ReadInItem(reader));
            }
            reader.Close();
            cmd.Dispose();
            conn.Close();
            return items;
        }

        /*
         * Returns a List containing all PLUs
         */
        public List<string> GetAllPLUs()
        {
            List<string> pLUs = new List<string>();
            string commandString = "SELECT " + UPCColumn + " FROM " + TableName;
            MySqlConnection conn = OpenConnection();
            MySqlCommand cmd = new MySqlCommand(commandString, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                pLUs.Add(reader.GetValue(0).ToString());
            }
            return pLUs;
        }

        /*
         * Read in a single Item from the DB
         */
        private Item ReadInItem(MySqlDataReader reader)
        {
            Item item = new Item();
            item.ItemID = reader.GetInt64(ItemIDColumn);
            item.UPC = reader.GetString(UPCColumn);
            item.Name = reader.GetString(NameColumn);
            item.Quantity = reader.GetInt32(QuantityColumn);
            item.Price = reader.GetFloat(PriceColumn);
            item.Category = null;
            return item;
        }
    }
}

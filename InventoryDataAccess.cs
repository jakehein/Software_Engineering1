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
            string commandString = "SELECT EXISTS(SELECT 1 FROM {TABLE_NAME} WHERE UPC = '" + upc + "' LIMIT 1)";
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
                string commandString = "INSERT INTO {TABLE_NAME} VALUES('" + "{add values}" + "')";
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
            if (DoesUPCExist(/*item.UPC*/""))
            {
                MySqlConnection conn = OpenConnection();
                string commandString = "UPDATE {TABLE_NAME} SET " + 
                                       "{FIRST_COLUMN_NAME} = '" + "" +
                                       "', {SECOND_COLUMN_NAME} = '" + "" +
                                       "', {THIRD_COLUMN_NAME} = '" + "" +
                                       "' WHERE {ID} = '" + "" + "'";
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
            string commandString = "DELETE FROM {TABLE_NAME} WHERE {ID} = '" + "" + "'";
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
            string commandString = "SELECT * FROM {TABLE_NAME} WHERE {ID} = '" + pLU + "' LIMIT 1";
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
            string commandString = "SELECT * FROM {TABLE_NAME}";
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
            string commandString = "SELECT {PLU} FROM {TABLE_NAME}";
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
            // read in item fields
            return new Item(/*set item parameters*/);
        }
    }
}

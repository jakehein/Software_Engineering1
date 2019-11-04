using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1.DataAccess
{
    class UserDataAccess : IUserDataAccess
    {
        private string connectionStringToDB = ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString;

        private const string userTable = "user";
        private const string pinColumn = "pin";
        private const string usernameColumn = "username";
        private const string hasInventoryAccessColumn = "has_inventory_access";
        
        public void CreateUser(string username, string pin, bool hasInventoryAccess)
        {
            string commandString = $@"INSERT INTO {userTable}
                                      ({usernameColumn}, {pinColumn}, {hasInventoryAccessColumn})
                                      VALUES (@Username, @Pin, @HasInventoryAccess);";
            using (MySqlConnection conn = new MySqlConnection(connectionStringToDB))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(commandString, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Pin", pin);
                cmd.Parameters.AddWithValue("@HasInventoryAccess", hasInventoryAccess);
                cmd.ExecuteNonQuery();
            }
        }

        public string GetPassword(string username)
        {
            string commandString = $@"SELECT {pinColumn}
                                      FROM {userTable}
                                      WHERE {usernameColumn} = @Username";
            using (MySqlConnection conn = new MySqlConnection(connectionStringToDB))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(commandString, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                MySqlDataReader rdr = cmd.ExecuteReader();
                return cmd.ExecuteScalar().ToString();
            }
        }

        public void UpdatePassword(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}

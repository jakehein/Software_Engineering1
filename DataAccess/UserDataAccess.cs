using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    class UserDataAccess : IUserDataAccess
    {
        private string connectionStringToDB = ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString;

        private const string userTable = "user";
        private const string passwordColumn = "password";
        private const string usernameColumn = "username";
        private const string hasManagerAccessColumn = "has_manager_access";
        
        public void CreateUser(string username, string password, bool hasManagerAccess)
        {
            string commandString = $@"INSERT INTO {userTable}
                                      ({usernameColumn}, {passwordColumn}, {hasManagerAccessColumn})
                                      VALUES (@Username, @Password, @HasManagerAccess);";
            using (MySqlConnection conn = new MySqlConnection(connectionStringToDB))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(commandString, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@HasManagerAccess", hasManagerAccess);
                cmd.ExecuteNonQuery();
            }
        }

        public string GetPassword(string username)
        {
            string commandString = $@"SELECT {passwordColumn}
                                      FROM {userTable}
                                      WHERE {usernameColumn} = @Username";
            using (MySqlConnection conn = new MySqlConnection(connectionStringToDB))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(commandString, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                return cmd.ExecuteScalar()?.ToString();
            }
        }

        public void UpdatePassword(string username, string password)
        {
            string commandString = $@"UPDATE {userTable}
                                      SET {passwordColumn} = @Password
                                      WHERE {usernameColumn} = @Username;";
            using (MySqlConnection conn = new MySqlConnection(connectionStringToDB))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(commandString, conn);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.ExecuteNonQuery();
            }
        }

        public bool DoesUserExist(string username)
        {
            string commandString = $@"SELECT {usernameColumn}
                                      FROM {userTable}
                                      WHERE {usernameColumn} = @Username";
            using (MySqlConnection conn = new MySqlConnection(connectionStringToDB))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(commandString, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                Object obj = cmd.ExecuteScalar();
                return obj != null;
            }
        }

        public bool DoesUserHaveInvetoryAccess(string username)
        {
            string commandString = $@"SELECT {hasManagerAccessColumn}
                                      FROM {userTable}
                                      WHERE {usernameColumn} = @Username";
            using (MySqlConnection conn = new MySqlConnection(connectionStringToDB))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(commandString, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                Object obj = cmd.ExecuteScalar();
                return (bool)obj;
            }
        }
    }
}

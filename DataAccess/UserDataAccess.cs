using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

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
            string hashedPassword = HashPassword(password);
            string commandString = $@"INSERT INTO {userTable}
                                      ({usernameColumn}, {passwordColumn}, {hasManagerAccessColumn})
                                      VALUES (@Username, @Password, @HasManagerAccess);";
            using (MySqlConnection conn = new MySqlConnection(connectionStringToDB))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(commandString, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", hashedPassword);
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

        /// <summary>
        /// Get the hashed password for the username entered and return if they match
        /// </summary>
        /// <param name="username">User entered username</param>
        /// <param name="password">User entered password</param>
        /// <returns>True if the user entered password and hashed password for the user from the DB are equal</returns>
        public bool ValidateUser(string username, string password)
        {
            string hashedPassword = GetPassword(username);
            return ValidatePassword(hashedPassword, password);
        }

        /// <summary>
        /// Create a hash from a user entered password
        /// </summary>
        /// <param name="password">The user entered password</param>
        /// <returns>The hash for the password</returns>
        private string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;
        }

        /// <summary>
        /// Check a userentered password against hashed password
        /// </summary>
        /// <param name="hashedPassword">The hashed password from DB</param>
        /// <param name="userEnteredPassword">The password entered by the user</param>
        /// <returns>true if passwords match</returns>
        private bool ValidatePassword(string hashedPassword, string userEnteredPassword)
        {
            try
            {
                byte[] hashBytes = Convert.FromBase64String(hashedPassword);
                /* Get the salt */
                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);
                /* Compute the hash on the password the user entered */
                var pbkdf2 = new Rfc2898DeriveBytes(userEnteredPassword, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);
                /* Compare the results */
                for (int i = 0; i < 20; i++)
                {
                    if (hashBytes[i + 16] != hash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}

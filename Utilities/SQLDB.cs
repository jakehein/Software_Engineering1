using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace FinalProject1
{
    public abstract class SQLDB
    {
        private readonly string connectionStringToDB = ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString;
        private MySqlConnection conn = null;
        private MySqlCommand cmd = null;
        private MySqlDataReader reader = null;
        protected MySqlDataReader Reader { get { return reader; } }

        /// <summary>
        /// Open the connection to the Database
        /// </summary>
        private void OpenConnection()
        {
            conn = new MySqlConnection(connectionStringToDB);
            conn.Open();
        }

        /// <summary>
        /// Turn a SQL command string int a MySQLCommand
        /// </summary>
        /// <param name="commandString">SQL string</param>
        private void BuildSqlCommand(string commandString)
        {
            OpenConnection();
            cmd = new MySqlCommand(commandString, conn);
        }

        /// <summary>
        /// Call BuildSqlCommand and Execute the command
        /// </summary>
        /// <param name="commandString">SQL string command to be executed</param>
        /// <returns>Number of affected rows</returns>
        protected int ExecuteNonQuery(string commandString)
        {
            BuildSqlCommand(commandString);
            int result = cmd.ExecuteNonQuery();
            CleanUp();
            return result;
        }

        /// <summary>
        /// Uses a command string to execute a 
        /// </summary>
        /// <param name="commandString">SQL string command to be executed</param>
        /// <returns>Result from the executed command</returns>
        protected object ExecuteScalar(string commandString)
        {
            BuildSqlCommand(commandString);
            object result = cmd.ExecuteScalar();
            CleanUp();
            return result;
        }

        protected string HandleApostrophe(string inString)
        {
            string s = "";
            string[] split = inString.Split('\'');
            if(split.Length > 1)
            {
                for(int i = 0; i < split.Length - 1; i++)
                {
                    s += split[i] + "\\\'";
                }
                s += split[split.Length - 1];
                return s;
            }
            else
            {
                return inString;
            }
        }

        /// <summary>
        /// Call the buildSqlCommand and set the reader
        /// </summary>
        /// <param name="commandString">SQL string command to be executed</param>
        protected void ExecuteReader(string commandString)
        {
            BuildSqlCommand(commandString);
            reader = cmd.ExecuteReader();
        }

        /// <summary>Clean-up all database access items</summary>
        protected void CleanUp()
        {
            if (reader != null)
            {
                reader.Close();
            }
            if (cmd != null)
            {
                cmd.Dispose();
            }
            if (conn != null)
            {
                conn.Close();
            }
        }
    }
}

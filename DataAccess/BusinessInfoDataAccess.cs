using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace FinalProject1
{
    class BusinessInfoDataAccess : IBusinessInfoDataAccess
    {
        private string connectionStringToDB = ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString;

        // Table identifier
        private const string BusinessTableName = "business_info";

        // Column identifiers
        private const string NameColumn = "company_name",
                             AddressColumn = "address",
                             StateTaxColumn = "state_tax",
                             LocalTaxColumn = "local_tax",
                             CashWarningColumn = "cash_warning_level",
                             PhoneColumn = "phone",
                             StartCashColumn = "starting_cash";

        /// <summary>
        /// Update the Business Info stored in the DB
        /// </summary>
        /// <param name="name">The Company Name</param>
        /// <param name="address">The Company Address</param>
        /// <param name="stateTax">The State Tax</param>
        /// <param name="localTax">The Local Tax</param>
        /// <param name="cashWarning">The Cash Warning Amount</param>
        /// <param name="startingCash">The Default Starting Cash Amount</param>
        /// <param name="phone">The Company Phone Number</param>
        /// <returns>true if the DB was updated</returns>
        public bool UpdateBusinessInfo(string name, string address, decimal stateTax, decimal localTax, decimal? cashWarning, decimal? startingCash, string phone)
        {
            int result = -1;
             
            using (MySqlConnection conn = new MySqlConnection(connectionStringToDB))
            {
                conn.Open();
                string commandString;
                string testForEmptyCommandString = "SELECT EXISTS (SELECT 1 FROM table)";
                MySqlCommand cmd = new MySqlCommand(testForEmptyCommandString, conn);

                if (IsDBTableEmpty())
                {
                    commandString = $@"INSERT INTO {BusinessTableName}(
                                                   {NameColumn},
                                                   {AddressColumn},
                                                   {StateTaxColumn},
                                                   {LocalTaxColumn},
                                                   {CashWarningColumn},
                                                   {PhoneColumn},
                                                   {StartCashColumn})
                                       VALUES(@Name, @Address, @StateTax, @LocalTax, @CashWarning, @Phone, @StartingCash)";
                }
                else
                {
                    commandString = $@"UPDATE {BusinessTableName}
                                              SET {NameColumn} = @Name,
                                                  {AddressColumn} = @Address,
                                                  {StateTaxColumn} = @StateTax,
                                                  {LocalTaxColumn} = @LocalTax,
                                                  {CashWarningColumn} = @CashWarning,
                                                  {PhoneColumn} = @Phone,
                                                  {StartCashColumn} = @StartingCash";
                }
                cmd.Dispose();

                cmd = new MySqlCommand(commandString, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@StateTax", stateTax);
                cmd.Parameters.AddWithValue("@LocalTax", localTax);
                cmd.Parameters.AddWithValue("@CashWarning", cashWarning);
                cmd.Parameters.AddWithValue("@StartingCash", startingCash);
                cmd.Parameters.AddWithValue("@Phone", phone);
                result = int.Parse(cmd.ExecuteNonQuery().ToString());
                cmd.Dispose();                
            }
            return result > 0;
        }

        /// <summary>
        /// Check if the BusinessInfo Table is empty
        /// </summary>
        /// <returns>true if the table is empty</returns>
        public bool IsDBTableEmpty()
        {
            int result = 0;
            using (MySqlConnection conn = new MySqlConnection(connectionStringToDB))
            {
                conn.Open();
                string testForEmptyCommandString = "SELECT EXISTS (SELECT 1 FROM table)";
                MySqlCommand cmd = new MySqlCommand(testForEmptyCommandString, conn);
                result = int.Parse(cmd.ExecuteScalar().ToString());
                cmd.Dispose();
            }
            return result <= 0;
        }

        public string Name { 
            get {
                return GetValueFromDB(NameColumn);
            }
        }
        public string Address
        {
            get
            {
                return GetValueFromDB(AddressColumn);
            }
        }
        public decimal TaxAmount
        {
            get
            {
                decimal taxAmount = Decimal.Parse(GetValueFromDB(LocalTaxColumn));
                taxAmount += Decimal.Parse(GetValueFromDB(StateTaxColumn));
                return taxAmount;
            }
        }
        public decimal? CashWarningLevel
        {
            get
            {
                if (Decimal.TryParse(GetValueFromDB(CashWarningColumn), out decimal cashWarning))
                {
                    return cashWarning;
                }
                else
                {
                    return 0.0M;
                }
            }
        }

        public decimal? StartingCashAmount
        {
            get
            {
                if (Decimal.TryParse(GetValueFromDB(StartCashColumn), out decimal startingCash))
                {
                    return startingCash;
                }
                else
                {
                    return -1.0M;
                }
            }
        }
        public string Phone
        {
            get
            {
                return GetValueFromDB(PhoneColumn);
            }
        }

        /// <summary>
        /// Retrieve info from the DB based on columnName
        /// </summary>
        /// <param name="columnName">The column name to get data from</param>
        /// <returns>The data stored in the DB as a string</returns>
        private string GetValueFromDB(string columnName)
        {
            string commandString = "SELECT " + columnName +
                                   " FROM " + BusinessTableName +
                                   " LIMIT 1";
            string databaseResult = "";
            
            using (MySqlConnection conn = new MySqlConnection(connectionStringToDB))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(commandString, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if(reader[0] != DBNull.Value)
                    {
                        databaseResult = reader[0].ToString();
                    }
                }
                cmd.Dispose();
            }
            return databaseResult;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FinalProject1
{
    public class User
    {
        private string username;
        public string Username 
        {
            get { return this.username; }
            set
            {
                if(value.Length <= 50 && value.Length >=5)
                {
                    username = value;
                }
                else
                {
                    DataErrors.Add("Invalid Username");
                }
            }
        }

        private string password;
        public string Password
        {
            get { return this.password; }
            set
            {
                if (value.Length <= 20 &&
                    value.Length >= 5 &&
                    Regex.Matches(value, @"^.*(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).*$").Count == 1)
                {
                    password = value;
                }
                else
                {
                    DataErrors.Add("Invalid Password");
                }
            }
        }

        public bool HasInventoryAccess { get; set; } = false;

        public List<string> DataErrors { get; set; } = new List<string>();
    }
}

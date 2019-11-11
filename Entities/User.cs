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

        private string pIN;
        public string PIN
        {
            get { return this.pIN; }
            set
            {
                if (value.Length <= 11 && 
                    value.Length >= 5 &&
                    Regex.Matches(value, @"^[0-9]{5,11}$").Count == 1)
                {
                    pIN = value;
                }
                else
                {
                    DataErrors.Add("Invalid PIN");
                }
            }
        }

        public bool HasInventoryAccess { get; set; } = false;

        public List<string> DataErrors { get; set; } = new List<string>();
    }
}

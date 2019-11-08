using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    class BusinessInfo
    {
        static string _name = "";
        static string _address = "";
        static decimal _taxAmount = 0.0M;
        static decimal? _cashWarningLevel = 0.0M;
        static decimal? _startingCashAmount = 0.0M;
        static string _phone = "";

        public static string Name { get { return _name; } }
        public static string Address { get { return _address; } }
        public static decimal TaxAmount { get { return _taxAmount; } }
        public static decimal? CashWarningLevel { get { return _cashWarningLevel; } }
        public static decimal? StartingCashAmount { get { return _startingCashAmount; } }
        public static string Phone { get { return _phone; } }

        private BusinessInfo() { }
        public BusinessInfo(IBusinessInfoDataAccess iBIDA)
        {
            _name = iBIDA.Name;
            _address = iBIDA.Address;
            _taxAmount = iBIDA.TaxAmount;
            _cashWarningLevel = iBIDA.CashWarningLevel;
            _startingCashAmount = iBIDA.StartingCashAmount;
            _phone = iBIDA.Phone;
        }
    }
}

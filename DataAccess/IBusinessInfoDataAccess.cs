using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    interface IBusinessInfoDataAccess
    {
        string Name { get; }
        string Address { get; }
        decimal TaxAmount { get; }
        decimal? CashWarningLevel { get; }
        decimal? StartingCashAmount { get; }
        string Phone { get; }

        bool UpdateBusinessInfo(string name, string address, decimal stateTax, decimal localTax, decimal? cashWarning, decimal? startingCash, string phone);
    }
}

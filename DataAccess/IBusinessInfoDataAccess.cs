using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1.DataAccess
{
    interface IBusinessInfoDataAccess
    {
        string Name { get; set; }
        string Address { get; set; }
        decimal TaxAmount { get; set; }
        decimal? CashWarningLevel { get; set; }
        decimal? StartingCashAmount { get; set; }
        string Phone { get; set; }
    }
}

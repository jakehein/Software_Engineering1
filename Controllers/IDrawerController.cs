using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1.Controllers
{
    interface IDrawerController
    {

        /// <summary>
        /// Cashes out a purchase for the given total and provides the correct change
        /// </summary>
        /// <param name="cashIn">Money being put into the drawer</param>
        /// <param name="purchaseTotal">The total of the purchase being cashed out</param>
        /// <returns></returns>
        decimal CashOut(decimal cashIn, decimal purchaseTotal);

        /// <summary>
        /// Adds the given amount of money to the Drawer and returns the new total
        /// </summary>
        /// <param name="moneyToAdd">The amount of money to add to the drawer</param>
        /// <returns>The new total amount of money in the drawer</returns>
        decimal AddToDrawer(decimal moneyToAdd);

        /// <summary>
        /// Returns the current cash in the drawer
        /// </summary>
        /// <returns>The current amount of money in the drawer</returns>
        decimal CurrentCashInDrawer();

        /// <summary>
        /// Removes the current drawer and creates a new one
        /// </summary>
        void ResetDrawer();
    }
}

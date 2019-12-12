using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
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

        /// <summary>
        /// Takes the given amount of money from the Drawer and returns the new total
        /// </summary>
        /// <param name="moneyToTake">The amount of money to take from the drawer</param>
        /// <returns>The new total amount of money in the drawer</returns>
        decimal WithdrawalFromDrawer(decimal moneyToTake);

        /// <summary>
        /// This method determines if the cash in the drawer is low.
        /// </summary>
        /// <returns>true if the cash in the drawer is less than or equal to the amount set as low</returns>
        bool cashIsLow();
        
        
        /// <summary>
        /// This method determines if the cash in the drawer is high.
        /// </summary>
        /// <returns>true if the cash in the drawer is more or equal to the amount set as high</returns>
        bool cashIsHigh();

    }
}

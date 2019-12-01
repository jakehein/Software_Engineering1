using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    class DrawerController : IDrawerController
    {

        /// <summary>
        /// Adds the given amount of money to the Drawer and returns the new total
        /// </summary>
        /// <param name="moneyToAdd">The amount of money to add to the drawer</param>
        /// <returns>The new total amount of money in the drawer or -1.0 if the add was invalid</returns>
        public decimal AddToDrawer(decimal moneyToAdd)
        {
            if(moneyToAdd > 0)
            {
                Drawer.drawer.AddToDrawer(moneyToAdd);
                return Drawer.drawer.Money;
            }
            return -1.0m;
        }

        /// <summary>
        /// Takes the given amount of money from the Drawer and returns the new total
        /// </summary>
        /// <param name="moneyToTake">The amount of money to take from the drawer</param>
        /// <returns>The new total amount of money in the drawer or -1.0 if the take was invalid</returns>
        public decimal WithdrawlFromDrawer(decimal moneyToTake)
        {
            if (moneyToTake > 0 && moneyToTake <= Drawer.drawer.Money)
            {
                Drawer.drawer.TakeFromDrawer(moneyToTake);
                return Drawer.drawer.Money;
            }
            return -1.0m;
        }

        /// <summary>
        /// Cashes out a purchase for the given total and provides the correct change
        /// </summary>
        /// <param name="cashIn">Money being put into the drawer</param>
        /// <param name="purchaseTotal">The total of the purchase being cashed out</param>
        /// <returns> The change to give back or -1.0 if the cash in was not enough to cover the purchase</returns>
        public decimal CashOut(decimal cashIn, decimal purchaseTotal)
        {
            decimal change = -1.0m;
            if(cashIn >= purchaseTotal)
            {
                change = cashIn - purchaseTotal;
                Drawer.drawer.AddToDrawer(purchaseTotal);
            }
            return change;
        }

        /// <summary>
        /// Returns the current cash in the drawer
        /// </summary>
        /// <returns>The amount of money currently in the drawer</returns>
        public decimal CurrentCashInDrawer()
        {
            return Drawer.drawer.Money;
        }

        /// <summary>
        /// Removes the current drawer and creates a new one
        /// </summary>
        public void ResetDrawer()
        {
            Drawer.drawer = new Drawer();
        }

        /// <summary>
        /// This method determines if cash is low in the drawer.
        /// </summary>
        /// <returns>true if the cash amount is below the specified lowCashAmount</returns>
        public bool cashIsLow()
        {
            decimal lowCashAmount = 50;
            if (Drawer.drawer.Money <= lowCashAmount)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

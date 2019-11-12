using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    class DrawerController : IDrawerController
    {
        Drawer drawer;
        public DrawerController()
        {
            drawer = new Drawer();
        }

        /// <summary>
        /// Adds the given amount of money to the Drawer and returns the new total
        /// </summary>
        /// <param name="moneyToAdd">The amount of money to add to the drawer</param>
        /// <returns>The new total amount of money in the drawer or -1.0 if the add was invalid</returns>
        public decimal AddToDrawer(decimal moneyToAdd)
        {
            if(moneyToAdd > 0)
            {
                drawer.AddToDrawer(moneyToAdd);
                return drawer.Money;
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
                drawer.AddToDrawer(purchaseTotal);
            }
            return change;
        }

        /// <summary>
        /// Returns the current cash in the drawer
        /// </summary>
        /// <returns>The amount of money currently in the drawer</returns>
        public decimal CurrentCashInDrawer()
        {
            return drawer.Money;
        }

        /// <summary>
        /// Removes the current drawer and creates a new one
        /// </summary>
        public void ResetDrawer()
        {
            drawer = new Drawer();
        }
    }
}

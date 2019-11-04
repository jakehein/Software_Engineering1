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
        /// Cashes out a purchase for the given total and provides the correct change
        /// </summary>
        /// <param name="cashIn">Money being put into the drawer</param>
        /// <param name="purchaseTotal">The total of the purchase being cashed out</param>
        /// <returns></returns>
        public decimal AddToDrawer(decimal moneyToAdd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds the given amount of money to the Drawer and returns the new total
        /// </summary>
        /// <param name="moneyToAdd">The amount of money to add to the drawer</param>
        /// <returns>The new total amount of money in the drawer</returns>
        public decimal CashOut(decimal cashIn, decimal purchaseTotal)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the current cash in the drawer
        /// </summary>
        /// <returns>The amount of money currently in the drawer</returns>
        public decimal CurrentCashInDrawer()
        {
            throw new NotImplementedException();
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    class Drawer
    {
        public static Drawer drawer = new Drawer();
        public decimal Money { get; set; }

        /// <summary>
        /// Increases the money by the amount specified in moneyIn
        /// </summary>
        /// <param name="moneyIn">the amount to increase the drawer by</param>
        public void AddToDrawer(decimal moneyIn)
        {
            Money += moneyIn;
        }

        /// <summary>
        /// Reduces the money by the amount specified in moneyOut
        /// </summary>
        /// <param name="moneyOut">the amount to decrease the drawer by</param>
        public void TakeFromDrawer(decimal moneyOut)
        {
            Money -= moneyOut;
        }
    }
}

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

        public void AddToDrawer(decimal moneyIn)
        {
            Money += moneyIn;
        }

        public void TakeFromDrawer(decimal moneyOut)
        {
            Money -= moneyOut;
        }
    }
}

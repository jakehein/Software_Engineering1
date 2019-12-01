using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1.Controllers
{
    class ControllerContainer
    {
        public ICartController CartController { get; set; }

        public ICategoryController CategoryController { get; set; }

        public IDrawerController DrawerController { get; set; }

        public IInventoryController InventoryController { get; set; }

        public IUserController UserController { get; set; }

    }
}

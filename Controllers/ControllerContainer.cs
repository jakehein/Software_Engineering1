using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    /// <summary>
    /// Container class for controllers
    /// </summary>
    class ControllerContainer
    {
        public ICartController CartController { get; set; }

        public ICategoryController CategoryController { get; set; }

        public IDrawerController DrawerController { get; set; }

        public IInventoryController InventoryController { get; set; }

        public IUserController UserController { get; set; }

        private ControllerContainer() {}

        private static ControllerContainer instance;

        /// <summary>
        /// Gets the instance of Controller container or creates a new one if one has not yet been initialized
        /// </summary>
        public static ControllerContainer Instance
        {
            get
            {
                if(instance == null)
                {
                    return new ControllerContainer();
                }
                else
                {
                    return instance;
                }
            }
        }
    }
}

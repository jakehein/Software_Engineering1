using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FinalProject1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        
        /// <summary>
        /// Main method for the application
        /// </summary>
        [STAThread]
        public static void Main()
        {
            // Setup implementations of controller interfaces
            ControllerContainer controllerContainer = ControllerContainer.Instance;
            controllerContainer.CartController = new CartController();
            controllerContainer.CategoryController = new CategoryController();
            controllerContainer.DrawerController = new DrawerController();
            controllerContainer.InventoryController = new InventoryController();
            controllerContainer.UserController = new UserController();

            // Setup implementations of data access interfaces
            DataAccessContainer dataAccessContainer = DataAccessContainer.Instance;
            dataAccessContainer.BusinessInfoDataAccess = new BusinessInfoDataAccess();
            dataAccessContainer.CategoryDataAccess = new CategoryDataAccess();
            dataAccessContainer.InventoryDataAccess = new InventoryDataAccess();
            dataAccessContainer.UserDataAccess = new UserDataAccess();
            dataAccessContainer.UserDataAccess = new UserDataAccess();

            var application = new App();
            application.InitializeComponent();
            application.Run();
        }
    }
}


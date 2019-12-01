using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    /// <summary>
    /// Container class for Data Access classes
    /// </summary>
    class DataAccessContainer
    {
        public IBusinessInfoDataAccess BusinessInfoDataAccess { get; set; }

        public ICategoryDataAccess CategoryDataAccess { get; set; }

        public IInventoryDataAccess InventoryDataAccess { get; set; }

        public IUserDataAccess UserDataAccess { get; set; }

        private DataAccessContainer() { }

        private static DataAccessContainer instance;

        /// <summary>
        /// Gets the instance of DataAccessContainer or creates a new one if one has not yet been initaialized
        /// </summary>
        public static DataAccessContainer Instance
        {
            get
            {
                if ( instance == null)
                {
                    return new DataAccessContainer();
                }
                else
                {
                    return instance;
                }
            }
        }

    }
}

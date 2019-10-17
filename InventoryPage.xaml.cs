using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FinalProject1
{
    /// <summary>
    /// Interaction logic for InventoryPage.xaml
    /// </summary>
    public partial class InventoryPage : UserControl
    {
        // what is InventoryDataAccess iDA variable for??
        IInventoryDataAccess iDA = new InventoryDataAccess();
        //InventoryDataAccess iDA = new IInventoryDataAccess();
        InventoryController inventoryControl = new InventoryController(iDA);

        public InventoryPage()
        {
            InitializeComponent();
            FillItemList();
        }

        /// <summary>
        /// This method populates the items scroll bar based on items in the inventory database.
        /// </summary>
        void FillItemList()
        {
            List<Item> itemList = inventoryControl.GetAllItems();

            foreach (Item item in itemList)
            {
                InventoryListBox.Items.Add(item);
            }
        }

        private void InventoryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void InventoryListCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LogOutBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}

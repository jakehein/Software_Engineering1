﻿using System;
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
    /// Interaction logic for SalesPage.xaml
    /// </summary>
    public partial class SalesPage : Page
    {
        private IInventoryController inventoryControl;
        private ICategoryController categoryControl;
        private ICartController cartController;
        private IDrawerController drawerController;
        private bool reading = false;
        private string scannerUPCString;
        private List<ItemDTO> itemDTOs = new List<ItemDTO>();
        public SalesPage()
        {
            InitializeComponent();
            categoryControl = new CategoryController(new CategoryDataAccess());
            inventoryControl = new InventoryController(new InventoryDataAccess(new CategoryDataAccess()));
            cartController = new CartController();
            drawerController = new DrawerController();
            PageSales.Focus();
            PopulateItemList();
            FillCategoryCombo();
            FillItemList(itemDTOs);
        }
        private void PopulateItemList()
        {
            itemDTOs = inventoryControl.GetAllItems();
        }
        void FillItemList(IEnumerable<ItemDTO> items)
        {
            Inventory.ItemsSource = items;
        }
        void FillCategoryCombo()
        {
            List<CategoryDTO> categories = categoryControl.GetAllCategories();
            //CategoryListCombo.ItemsSource = categoryControl.GetAllCategories();
            categories.Add(new CategoryDTO { CategoryID = 0, Items = null, Name = "All" });
            categories.Sort();
            Category.ItemsSource = categories;
            Category.SelectedIndex = 0;
        }
        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        /// <summary>
        /// This method clears the transaction from the cart.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            cartController.CancelTransaction();
            UpdateTransactionView();
            UpdateTotal();
        }

        private void Pay_Click(object sender, RoutedEventArgs e)
        {
            string total = PayTotal.Text;
            Transaction.ItemsSource = cartController.Checkout();
            MessageBox.Show("Total is: " + total);
        }

        /*
        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ItemDTO itm = (ItemDTO)Inventory.SelectedItem;
                cartController.AddItem(itm);
                UpdateTransactionView();
                UpdateTotal();
            }
            catch (Exception)
            {
                MessageBox.Show("Select an item to add to the transaction.");
            }
        }*/

        private void QuantityUp_Click(object sender, RoutedEventArgs e)
        {
            if (Transaction.SelectedItem != null)
            {
                ItemDTO itm = ((Cart.SalesItem)Transaction.SelectedItem).Item;
                cartController.AddItem(itm);
                UpdateTransactionView();
                UpdateTotal();
            }
            else
            {
                MessageBox.Show("Select an Item from the transaction to increase the quantity");
            }
        }

        /// <summary>
        /// Refresh the items in the Transaction ListView
        /// </summary>
        private void UpdateTransactionView()
        {
            int index = Transaction.SelectedIndex;
            Transaction.ItemsSource = null;
            Transaction.ItemsSource = cartController.GetAllItems();
            Transaction.SelectedIndex = index;
        }

        /// <summary>
        /// Decrease the quantity of an item in the Transaction ListView by 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuantityDown_Click(object sender, RoutedEventArgs e)
        {
            if (Transaction.SelectedItem != null)
            {
                ItemDTO item = ((Cart.SalesItem)Transaction.SelectedItem).Item;
                int quantity = ((Cart.SalesItem)Transaction.SelectedItem).Quantity;
                cartController.ChangeQuantity(quantity - 1, item);
                UpdateTransactionView();
                UpdateTotal();
            }
            else
            {
                MessageBox.Show("Select an Item from the transaction to decrease the quantity");
            }
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (Transaction.SelectedItem != null)
            {
                ItemDTO item = ((Cart.SalesItem)Transaction.SelectedItem).Item;
                cartController.RemoveItem(item);
                UpdateTransactionView();
                UpdateTotal();
            }
            else
            {
                MessageBox.Show("Select an Item from the transaction to remove");
            }
        }

        /// <summary>
        /// This method takes the UPC and searches to see if it is avaliable.
        /// </summary>
        private void UpCButton_Click(object sender, RoutedEventArgs e)
        {
            string upC = UpC.Text;
            ItemDTO itm = cartController.GetItem(upC);
            var items = itemDTOs.Cast<ItemDTO>().Where(x => x.UPC.Equals(upC));
            if (items.Count() > 0)
            {
                Category.SelectedIndex = -1;
                FillItemList(items);
                UpC.Text = "";
            }
            else
            {
                MessageBox.Show("Item with the entered UPC not found!!!");
            }
            //var items = item.Cast<ItemDTO>().Where(item => item.Category.CategoryID == ((CategoryDTO)e.AddedItems[0]).CategoryID);
            //IEnumerable<ItemDTO> item;
            //FillItemList(item);

        }

        private void UpC_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((TextBox)sender).FontSize = ((TextBox)sender).ActualHeight / 2;
        }

        private void PayTotal_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((TextBlock)sender).FontSize = ((TextBlock)sender).ActualHeight / 1.5;
        }

        private void Transaction_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TransactionItemColumn.Width = ((ListView)sender).ActualWidth * .6;
            TransactionPriceColumn.Width = ((ListView)sender).ActualHeight * .15;
            TransactionQuantityColumn.Width = ((ListView)sender).ActualWidth * .15;
        }

        private void Category_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((ComboBox)sender).FontSize = ((ComboBox)sender).ActualHeight * .5;
        }

        /// <summary>
        /// This method takes the selected item and adds it to the cart list
        /// </summary>
        private void Inventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ItemDTO itm = (ItemDTO)Inventory.SelectedItem;
            if (itm != null)
            {
                try
                {
                    cartController.AddItem(itm);
                    UpdateTransactionView();
                    UpdateTotal();
                    Inventory.SelectedItem = null;

                }
                catch (Exception)
                {
                    MessageBox.Show("Error. Please try again");
                }
            }
        }

        /// <summary>
        /// This method populates the items list with the selected category. 
        /// </summary>
        private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0 || ((CategoryDTO)e.AddedItems[0]).CategoryID == 0)
            {
                FillItemList(itemDTOs);
            }
            else
            {
                var items = itemDTOs.Cast<ItemDTO>().Where(item => item.Category.CategoryID == ((CategoryDTO)e.AddedItems[0]).CategoryID);
                FillItemList(items);
            }
        }

        private void UpdateTotal()
        {
            PayTotal.Text = String.Format("{0:#,###0.00}", cartController.GetTotal());
        }

        /// <summary>
        /// Check for barcode reader key combinations when keys are pressed is entered
        /// </summary>
        /// <param name="sender">Object that triggered the event</param>
        /// <param name="e">Event details</param>
        private void HandleBarcodeScan(object sender, KeyEventArgs e)
        {
            // leftCtrl + B focus upcText and set reading to true
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.B))
            {
                PageSales.Focus();
                scannerUPCString = "";
                reading = true;
            }
            // leftCtrl + J take the UPC from UPCText and find the item if it exists
            // otherwise clear fields and prepare for new item entry
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.J))
            {
                if (reading)
                {
                    // find the item associated with the upc entered
                    ItemDTO item = itemDTOs
                                    .Cast<ItemDTO>()
                                    .Where(x => x.UPC.Equals(scannerUPCString)).FirstOrDefault();
                    if (item != null)
                    {
                        cartController.AddItem(item);
                        UpdateTransactionView();
                        UpdateTotal();
                    }
                    else
                    {
                        MessageBox.Show("Item Not Found!!!");
                    }
                }
                reading = false;
            }
            //
            else if(reading && !Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                // get the character from the event item and add it to the current upc
                scannerUPCString += e.Key.ToString()[1];
            }
        }

        private void ChangeQuantity_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}

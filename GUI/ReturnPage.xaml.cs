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
    /// Interaction logic for ReturnPage.xaml
    /// </summary>
    public partial class ReturnPage : Page
    {
        private IInventoryController inventoryControl => ControllerContainer.Instance.InventoryController;
        private ICategoryController categoryControl => ControllerContainer.Instance.CategoryController;
        private ICartController cartController => ControllerContainer.Instance.CartController;
        private IDrawerController drawerController => ControllerContainer.Instance.DrawerController;
        private bool reading = false;
        private string scannerUPCString;
        private List<ItemDTO> itemDTOs = new List<ItemDTO>();
        public ReturnPage()
        {
            InitializeComponent();
            PageReturns.Focus();
            PopulateItemList();
            FillCategoryCombo();
            FillItemList(itemDTOs);
            cartController.CancelTransaction();
        }

        /// <summary>
        /// This method populates the items list with all items within the inventory.
        /// </summary>
        private void PopulateItemList()
        {
            itemDTOs = inventoryControl.GetAllItems();
        }

        /// <summary>
        /// Sets the ItemsSource for the Inventory element to the items parameter.
        /// </summary>
        /// <param name="items"></param>
        void FillItemList(IEnumerable<ItemDTO> items)
        {
            Inventory.ItemsSource = items;
        }

        /// <summary>
        /// Gets all the categories and sorts them before setting them to the Category element's ItemsSource.
        /// </summary>
        void FillCategoryCombo()
        {
            List<CategoryDTO> categories = categoryControl.GetAllCategories();
            categories.Add(new CategoryDTO { CategoryID = 0, Items = null, Name = "All" });
            categories.Sort();
            var sortedCategories = categories.OrderBy(x => x.Name);
            Category.ItemsSource = sortedCategories;
            Category.SelectedIndex = 0;
        }

        /// <summary>
        /// This method directs the user to the MainMenu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainMenu());
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

        /// <summary>
        /// Check that there is enough money in the till to perform a return.
        /// Afterwards, a check is made for a low cash warning.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            string total = PayTotal.Text;
            if (isEnoughMoneyInTill())
            {
                Transaction.ItemsSource = cartController.ReturnItems();
                drawerController.WithdrawalFromDrawer(decimal.Parse(total));
                MessageBox.Show("Refund total is: " + total);
                UpdateTotal();
            }
            // After cash is taken out this will determine if the cash left in the drawer is low
            LowCashWarningCheck();
        }

        /// <summary>
        /// Increases the quantity of an item in the Transaction ListView by 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuantityUp_Click(object sender, RoutedEventArgs e)
        {
            if (Transaction.SelectedItem != null)
            {
                ItemDTO itm = ((DTOs.SalesItemDTO)Transaction.SelectedItem).Item;
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
                ItemDTO item = ((DTOs.SalesItemDTO)Transaction.SelectedItem).Item;
                int quantity = ((DTOs.SalesItemDTO)Transaction.SelectedItem).Quantity;
                cartController.ChangeQuantity(quantity - 1, item);
                UpdateTransactionView();
                UpdateTotal();
            }
            else
            {
                MessageBox.Show("Select an Item from the transaction to decrease the quantity");
            }
        }

        /// <summary>
        /// Removes all instances of an item from the Transaction ListView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (Transaction.SelectedItem != null)
            {
                ItemDTO item = ((DTOs.SalesItemDTO)Transaction.SelectedItem).Item;
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
                MessageBox.Show("Item with the entered UPC not found.");
            }
        }

        /// <summary>
        /// Changes the font size of the UpC.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpC_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((TextBox)sender).FontSize = ((TextBox)sender).ActualHeight / 2;
        }

        /// <summary>
        /// Changes the font size of PayTotal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PayTotal_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((TextBlock)sender).FontSize = ((TextBlock)sender).ActualHeight / 1.5;
        }

        /// <summary>
        /// Changes the size of the Transaction element columns.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Transaction_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TransactionItemColumn.Width = ((ListView)sender).ActualWidth * .65;
            TransactionPriceColumn.Width = ((ListView)sender).ActualWidth * .25;
            TransactionQuantityColumn.Width = ((ListView)sender).ActualWidth * .1;
        }

        /// <summary>
        /// Changes the font size of the Category list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Update the PayTotal element's text value.
        /// </summary>
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
                PageReturns.Focus();
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
            else if (reading && !Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                // get the character from the event item and add it to the current upc
                scannerUPCString += e.Key.ToString()[1];
            }
        }

        /// <summary>
        /// Change the quantity of the item selected by entering an amount via mouse controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (Transaction.SelectedItem != null)
            {
                GUI.ChangeQuantityOnClickDialog dialog = new GUI.ChangeQuantityOnClickDialog();
                bool? result = dialog.ShowDialog();
                if (result != null && (bool)result)
                {
                    ItemDTO item = ((DTOs.SalesItemDTO)Transaction.SelectedItem).Item;
                    int quantity = ((DTOs.SalesItemDTO)Transaction.SelectedItem).Quantity;
                    cartController.ChangeQuantity(int.Parse(dialog.Result), item);
                    UpdateTransactionView();
                    UpdateTotal();
                }
            }
            else
            {
                MessageBox.Show("Select an Item from the transaction to change the quantity");
            }
        }

        /// <summary>
        /// This method checks the amount of money present in the till and displays a warning to the user if it is determined
        /// as low.
        /// </summary>
        public void LowCashWarningCheck()
        {
            if (drawerController.cashIsLow())
            {
                decimal amountInDrawer = drawerController.CurrentCashInDrawer();
                MessageBox.Show("Cash Till is Running Low. Amount is currently: $" + amountInDrawer + ". Please Deposit More.");
            }
        }

        /// <summary>
        /// This method checks the amount of money present in the till and displays a warning to the user if it is determined
        /// as low.
        /// </summary>
        public bool isEnoughMoneyInTill()
        {
            decimal amountInDrawer = drawerController.CurrentCashInDrawer();
            decimal refundTotal = decimal.Parse(PayTotal.Text);
            if (amountInDrawer < refundTotal)
            {
                MessageBox.Show("Not enough money in the till to complete refund. Amount is currently: $" + amountInDrawer + ". Please Deposit More.");
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Change the quantity of the item selected by entering an amount via touch controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeQuantity_TouchUp(object sender, TouchEventArgs e)
        {
            if (Transaction.SelectedItem != null)
            {
                GUI.OnScreenKeyboard.OnScreenNumPad dialog = new GUI.OnScreenKeyboard.OnScreenNumPad("");
                bool? result = dialog.ShowDialog();
                if (result != null && (bool)result && !dialog.GetResult().Equals(""))
                {
                    ItemDTO item = ((DTOs.SalesItemDTO)Transaction.SelectedItem).Item;
                    int quantity = ((DTOs.SalesItemDTO)Transaction.SelectedItem).Quantity;
                    cartController.ChangeQuantity(int.Parse(dialog.GetResult()), item);
                    UpdateTransactionView();
                    UpdateTotal();
                }
            }
            else
            {
                MessageBox.Show("Select an Item from the transaction to change the quantity");
            }
        }
    }
}

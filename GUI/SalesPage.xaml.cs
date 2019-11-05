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
    /// Interaction logic for SalesPage.xaml
    /// </summary>
    public partial class SalesPage : Page
    {
        private IInventoryController inventoryControl;
        private ICategoryController categoryControl;
        private ICartController cartController;
        private IDrawerController drawerController;
        private bool reading = false;
        private List<ItemDTO> itemDTOs = new List<ItemDTO>();
        public SalesPage()
        {
            InitializeComponent();
            categoryControl = new CategoryController(new CategoryDataAccess());
            inventoryControl = new InventoryController(new InventoryDataAccess(new CategoryDataAccess()));
            cartController = new CartController();
            drawerController = new DrawerController();
            //UPCText.Focus();
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
            Transaction.Items.Clear();
            UpdateTotal();
        }

        private void Pay_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ItemDTO itm = (ItemDTO)Inventory.SelectedItem;
                cartController.AddItem(itm);
                Transaction.ItemsSource = cartController.GetAllItems();
                
            }
            catch (Exception)
            {
                MessageBox.Show("Select an item to add to the transaction.");
            }
        }

        private void QuantityUp_Click(object sender, RoutedEventArgs e)
        {
            ItemDTO itm = (ItemDTO)Transaction.SelectedItem;
            if (itm != null)
            {
                cartController.AddItem(itm);
                Transaction.Items.Add(itm);
                UpdateTotal();
            }
            else
            {
                MessageBox.Show("Select an Item from the transaction to increase the quantity");
            }
        }

        private void QuantityDown_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// This method takes the UPC and searches to see if it is avaliable.
        /// </summary>
        private void UpCButton_Click(object sender, RoutedEventArgs e)
        {
            string upC = UpC.Text;
            ItemDTO itm = cartController.GetItem(upC);
            //var items = itm.Cast<ItemDTO>();//.Where(item => item.Category.CategoryID == ((CategoryDTO)e.AddedItems[0]).CategoryID);
            //IEnumerable<ItemDTO> item = itm;
            //Inventory.Items.Clear();
            //Inventory.Items.Add(itm);1

        }

        private void UpC_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((TextBox)sender).FontSize = ((TextBox)sender).ActualHeight / 2;
        }

        private void PayTotal_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((TextBlock)sender).FontSize = ((TextBlock)sender).ActualHeight / 1.5;
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
                    Transaction.Items.Add(itm);
                    UpdateTotal();

                }
                catch (Exception)
                {
                    MessageBox.Show(":( Add Items still throws Exception");
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
            PayTotal.Text = cartController.GetTotal().ToString();
        }

        /*private void Inventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {+
            ItemDTO itm = (ItemDTO)Inventory.SelectedItem;
            if (itm != null)
            {
                
                //UPCText.Text = itm.UPC;
                //NameText.Text = itm.Name;
                //QuantityText.Text = itm.Quantity.ToString();
                //PriceText.Text = itm.Price.ToString();
                //CategoryListCombo.SelectedIndex = CategoryListCombo.Items.IndexOf(Category.createDTOFromCategory(itm.Category));
            }
        }*/

    }
}

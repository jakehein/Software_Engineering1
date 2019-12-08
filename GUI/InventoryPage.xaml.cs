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
using Microsoft.VisualBasic;

namespace FinalProject1
{
    /// <summary>
    /// Interaction logic for Inventory_Page.xaml
    /// </summary>
    public partial class InventoryPage : Page
    {
        private IInventoryController inventoryControl => ControllerContainer.Instance.InventoryController;
        private ICategoryController categoryControl => ControllerContainer.Instance.CategoryController;
        private bool reading = false;
        private List<ItemDTO> itemDTOs = new List<ItemDTO>();

        public InventoryPage()
        {
            InitializeComponent();
            UPCText.Focus();
            PopulateItemList();
            FillCategoryComboBox();
            FillItemList(itemDTOs);
        }

        /// <summary>
        /// Populates the item list with all the items within the inventory
        /// </summary>
        private void PopulateItemList()
        { 
            itemDTOs = inventoryControl.GetAllItems();
        }

        /// <summary>
        /// This method populates the items scrollable list based on items in the inventory database.
        /// </summary>
        void FillItemList(IEnumerable<ItemDTO> items)
        { 
            InventoryListBox.ItemsSource = items;
        }
        
        /// <summary>
        /// This method populates the combo item list in the inventory database. 
        /// </summary>
        void FillCategoryComboBox()
        {
            List<CategoryDTO> categories = categoryControl.GetAllCategories();
            //CategoryListComboBoxInput.ItemsSource = categoryControl.GetAllCategories();

            // create new category option
            UpdateCategoryListComboBoxInput();
            categories.Add(new CategoryDTO { CategoryID = 0, Items = null, Name = "All"});
            categories.Sort();
            var sortedCategories = categories.OrderBy(x => x.Name);
            CategoryListComboBox.ItemsSource = sortedCategories;
            CategoryListComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// This method updates/initalizes the comboBoxInput with all avaliable categories and one
        /// new category option.
        /// </summary>
        void UpdateCategoryListComboBoxInput()
        {
            List<CategoryDTO> categories = categoryControl.GetAllCategories();
            var sortedCategories = categories.OrderBy(x => x.Name);
            CategoryListComboBoxInput.ItemsSource = sortedCategories;
        }

        /// <summary>
        /// This method sets the information section with the data from the selected Item object
        /// </summary>
        private void InventoryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemDTO itm = (ItemDTO)InventoryListBox.SelectedItem;
            if (itm != null)
            {
                UPCText.Text = itm.UPC;
                NameText.Text = itm.Name;
                QuantityText.Text = itm.Quantity.ToString();
                PriceText.Text = itm.Price.ToString();
                CategoryListComboBoxInput.SelectedIndex = CategoryListComboBoxInput.Items.IndexOf(Category.createDTOFromCategory(itm.Category));
            }
        }

        /// <summary>
        /// When the category is changed the InventoryListBox is populated with items within the selected category.
        /// </summary>
        private void CategoryListComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
        /// This method verifies that the UPC does not previously exists then creates a new item 
        /// with the written item information.
        /// </summary>
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IsEnteredInventoryItemValid())
            {
                ItemDTO create = CreateItemObj();
                if (create != null) 
                {
                     bool created = inventoryControl.CreateItem(create);
                    if (created)
                    {
                        MessageBox.Show("Item was successfully created.");
                        ClearInputs();
                        UpdateInventoryList();
                    }
                    else
                    {
                        MessageBox.Show("Item could not be created. Please try again.");
                    }
                }
            }
            else
            {
                MessageBox.Show("UPC is already in use or invalid data. Please try again.");
            }
        }

        /// <summary>
        /// This method verifies that the UPC previously exists then updates the product with the
        /// new item information.
        /// </summary>
        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IsEnteredInventoryItemValid())
            {
                ItemDTO update = GetItemObj();

                if (((ItemDTO)InventoryListBox.SelectedItem) != null){
                    string uPC = ((ItemDTO)InventoryListBox.SelectedItem).UPC;
                    bool updated = inventoryControl.UpdateItem(uPC, update);
                    if (updated)
                    {
                        MessageBox.Show("Item was successfully updated.");
                        ClearInputs();
                        UpdateInventoryList();
                    }
                    else
                    {
                        MessageBox.Show("Item could not be updated. Please try again.");
                    }
                } 
            }
            else
            {
                MessageBox.Show("Invalid Inputs. Please try again.");
            }
        }

        /// <summary>
        /// This method verifies that the UPC previously exists then deletes the product from the inventory.
        /// Then updates the item list to reflect these changes.
        /// </summary>
        /// <param name="sender">Object that triggered the event</param>
        /// <param name="e">Event details</param>
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            string uPC = UPCText.Text;
            bool deleted = inventoryControl.DeleteItem(uPC);
            
            if (deleted)
            {
                MessageBox.Show("Item was successfully deleted.");
                ClearInputs();
                UpdateInventoryList();
            }
            else
            {
                MessageBox.Show("UPC is invalid. Please try again.");
            }
        }

        /// <summary>
        /// This method navigates the user from the inventory page to the Main Menu.
        /// </summary>
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainMenu());
        }

        /// <summary>
        /// This method clears all inputs from the information side as well as resetting the values of the
        /// list box.
        /// </summary>
        private void ClearInputs()
        {
            UPCText.Text = "";
            NameText.Text = "";
            QuantityText.Text = "";
            PriceText.Text = "";
            CategoryListComboBox.SelectedIndex = 0;
            CategoryListComboBoxInput.SelectedIndex = -1;
            InventoryListBox.SelectedIndex = -1;
        }

        /// <summary>
        /// This method updates all items in the ListBox and ListCombo.
        /// </summary>
        private void UpdateInventoryList()
        {
            PopulateItemList();
            FillItemList(itemDTOs);
        }

        /// <summary>
        /// This method takes information from the text boxs and converts them into an item object.
        /// </summary>
        /// <returns></returns> Item based of inputs within the information section of the GUI
        private ItemDTO GetItemObj()
        {
            ItemDTO itm = new ItemDTO();
            try
            {
                Category category = null;
                if (CategoryListComboBoxInput.SelectedIndex != -1)
                {
                    category = Category.createCategoryFromDTO((CategoryDTO)CategoryListComboBoxInput.SelectedItem);
                }
                string uPC = UPCText.Text;
                string name = NameText.Text;
                int quantity = int.Parse(QuantityText.Text);
                decimal price = decimal.Parse(PriceText.Text);
                itm.ItemID = ((ItemDTO)InventoryListBox.SelectedItem).ItemID; 
                itm.UPC = uPC;
                itm.Name = name;
                itm.Category = category;
                itm.Quantity = quantity;
                itm.Price = price;

                return itm;
            }
            catch (FormatException)
            {
                MessageBox.Show("Format is invald. Please try again");
            }
            return itm;
        }


        /// <summary>
        /// This method creates an Item Object specifically for creating 
        /// </summary>
        /// <returns> ItemDTO based of inputs of the user </returns> 
        private ItemDTO CreateItemObj()
        {
            ItemDTO itm = new ItemDTO();

            try
            {
                Category category = null;
                if (CategoryListComboBoxInput.SelectedIndex != -1)
                {
                    category = Category.createCategoryFromDTO((CategoryDTO)CategoryListComboBoxInput.SelectedItem);
                }
                string uPC = UPCText.Text;
                string name = NameText.Text;
                int quantity = int.Parse(QuantityText.Text);
                decimal price = decimal.Parse(PriceText.Text);
                itm.UPC = uPC;
                itm.Name = name;
                itm.Category = category;
                itm.Quantity = quantity;
                itm.Price = price;

                return itm;
            }
            catch (FormatException)
            {
                MessageBox.Show("Format is invald. Please try again");
            }
            return itm;
        }

        /// <summary>
        /// This method we validate the user input against specifications for neccessary item values.
        /// </summary>
        /// <param name="itm"></param> itm we are checking the parameters of
        /// <returns>true if inputs for parameters are all valid</returns> 
        private bool IsEnteredInventoryItemValid()
        {
            List<bool> val = new List<bool>();
            string nullText = "";
            //check if any values are null
            if (UPCText.Text == nullText)
            {
                return false;
            }
            else if (NameText.Text == nullText)
            {
                return false;
            }
            else if (PriceText.Text == nullText)
            {
                return false;
            }
            else if (QuantityText.Text == nullText)
            {
                return false;
            }

            val.Add(AreAllDigits(UPCText.Text));

            // Verify Quantity is all Digits
            val.Add(AreAllDigits(QuantityText.Text));

            // Verify Price follows a Valid format
            val.Add(IsValidPriceFormat(PriceText.Text));

            // Verify the Category is valid
            val.Add(IsValidCategory((CategoryDTO)CategoryListComboBoxInput.SelectedItem));

            foreach (bool value in val)
            {
                if (!value)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Check the string to see if all characters present are digits. Returns true if all characters are digits.
        /// </summary>
        /// <param name="digits"> string from the information box to see if all characters are digits </param> 
        /// <returns> true if all characters are digits </returns> 
        private bool AreAllDigits(string digits)
        {
            char[] charArr = digits.ToCharArray();

            foreach (char character in charArr)
            {
                if (!Char.IsNumber(character))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// This method checks the string price to see if follows the format of a valid price. i.e. xxx.xx
        /// </summary>
        /// <param name="price"> The string value of the price </param>
        /// <returns> True if the format is valid. </returns>
        private bool IsValidPriceFormat(string price)
        {
            decimal cashDecimal;
            bool isDecimal = decimal.TryParse(price, out cashDecimal);

            if (isDecimal && (cashDecimal >= 0) && (cashDecimal * 100 == Math.Floor(cashDecimal * 100)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// This method verifies the contents of category
        /// </summary>
        /// <param name="category">category is the category we are checking</param>
        /// <returns>true if category is valid</returns>
        private bool IsValidCategory(CategoryDTO category)
        {
            return (category != null);
        }

        /// <summary>
        /// Check for barcode reader key combinations when textcomposition is entered
        /// </summary>
        /// <param name="sender">Object that triggered the event</param>
        /// <param name="e">Event details</param>
        private void HandleBarcodeScan(object sender, TextCompositionEventArgs e)
        {
            // leftCtrl + B focus upcText and set reading to true
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.B))
            {
                UPCText.Text = "";
                UPCText.Focus();
                reading = true;
            }
            // leftCtrl + J take the UPC from UPCText and find the item if it exists
            // otherwise clear fields and prepare for new item entry
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.J))
            {
                if (reading)
                {
                    // find the item associated with the upc entered
                    ItemDTO lItem = itemDTOs
                                    .Cast<ItemDTO>()
                                    .Where(item => item.UPC.Equals(UPCText.Text)).FirstOrDefault();
                    if (lItem != null)
                    {
                        CategoryListComboBox.SelectedIndex = CategoryListComboBox.Items.IndexOf(Category.createDTOFromCategory(lItem.Category));
                        InventoryListBox.SelectedIndex = InventoryListBox.Items.IndexOf(lItem);
                    }
                    else
                    {
                        InventoryListBox.SelectedIndex = -1;
                        string upc = UPCText.Text;
                        ClearInputs();
                        UPCText.Text = upc;
                    }
                }
                reading = false;
            }
        }


        /// <summary>
        /// Navigates from Inventory Page to the Category Manager which allows the manager to 
        /// create, update, and delete categories.
        /// </summary>
        /// <param name="sender">Object that triggered the event</param>
        /// <param name="e">Event details</param>
        private void CategoryManagerBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CategoryManager());
        }
    }
}

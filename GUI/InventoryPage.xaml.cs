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
    /// Interaction logic for Inventory_Page.xaml
    /// </summary>
    public partial class InventoryPage : Page
    {

        IInventoryController inventoryControl;
        ICategoryController categoryControl;
        bool reading = false;

        public InventoryPage()
        {
            InitializeComponent();
            categoryControl = new CategoryController(new CategoryDataAccess());
            inventoryControl = new InventoryController(new InventoryDataAccess(new CategoryDataAccess()));
            UPCText.Focus();
            FillItemCombo();
            FillItemList();
        }

        /// <summary>
        /// This method populates the items scrollable list based on items in the inventory database.
        /// </summary>
        void FillItemList()
        {
            List<ItemDTO> itemList = inventoryControl.GetAllItems();

            foreach (ItemDTO item in itemList)
            {
                InventoryListBox.Items.Add(item);
            }
        }

        /// <summary>
        /// This method populates the combo item list in the inventory database. 
        /// </summary>
        void FillItemCombo()
        {
            {
                CategoryListCombo.ItemsSource = categoryControl.GetAllCategories();
            }
        }

        /// <summary>
        /// This method sets the information section with the data from the Item object
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
                CategoryListCombo.SelectedIndex = CategoryListCombo.Items.IndexOf(Category.createDTOFromCategory(itm.Category));
            }
        }

        /// <summary>
        /// This method sets the information with the data from the Item object
        /// </summary>
        private void InventoryListCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = InventoryListCombo.Text;
            ItemDTO itm = inventoryControl.GetItem(text);

            UPCText.Text = itm.UPC;
            NameText.Text = itm.Name;
            QuantityText.Text = itm.Quantity.ToString();
            PriceText.Text = itm.Price.ToString();

            CategoryListCombo.SelectedItem = itm.Category;
        }

        /// <summary>
        /// This method verifies that the UPC does not previously exists then creates a new item 
        /// with the item information.
        /// </summary>
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            ItemDTO create = CreateItemObj();

            if (Valid())
            {
                Boolean created = inventoryControl.CreateItem(create);
                if (created)
                {
                    MessageBox.Show("Item was successfully created.");
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Item could not be created. Please try again.");
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
            ItemDTO update = GetItemObj();
            string uPC = UPCText.Text;

            if (Valid())
            {
                bool updated = inventoryControl.UpdateItem(uPC, update);
                if (updated)
                {
                    MessageBox.Show("Item was successfully updated.");
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Item could not be updated. Please try again.");
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
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            string uPC = UPCText.Text;
            Boolean deleted = inventoryControl.DeleteItem(uPC);

            if (deleted)
            {
                MessageBox.Show("Item was successfully deleted.");
                ClearInputs();
            }
            else
            {
                MessageBox.Show("UPC is invalid. Please try again.");
            }
        }

        /// <summary>
        /// This method exits the inventory view and returns to the Main Menu.
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
            CategoryListCombo.SelectedIndex = -1;
            InventoryListBox.SelectedIndex = -1;
            UpdateList();
        }

        /// <summary>
        /// This method updates all items in the ListBox and List Combo.
        /// </summary>
        private void UpdateList()
        {
            InventoryListBox.Items.Clear();
            FillItemList();
            InventoryListCombo.Items.Clear();
            FillItemCombo();
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
                Category category = Category.createCategoryFromDTO((CategoryDTO)CategoryListCombo.SelectedItem);

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
            catch (System.FormatException ex)
            {
                MessageBox.Show("Format is invald. Please try again");
            }
            return itm;
        }


        /// <summary>
        /// This method helps create an Item Object specifically for creating 
        /// </summary>
        /// <returns></returns> Item based of inputs of the user
        private ItemDTO CreateItemObj()
        {
            ItemDTO itm = new ItemDTO();

            try
            {
                Category category = Category.createCategoryFromDTO((CategoryDTO)CategoryListCombo.SelectedItem);

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
            catch (System.FormatException ex)
            {
                MessageBox.Show("Format is invald. Please try again");
            }
            return itm;
        }

        /// <summary>
        /// This method we validate the user input against specifications for neccessary item values.
        /// </summary>
        /// <param name="itm"></param> itm we are checking the parameters of
        /// <returns></returns> true if inputs for parameters are all good
        private bool Valid()
        {
            List<bool> val = new List<bool>();

            //check if any values are null
            if (UPCText.Text == null)
            {
                val.Add(false);
            }
            else if (NameText.Text == null)
            {
                val.Add(false);
            }
            else if (PriceText.Text == null)
            {
                val.Add(false);
            }
            else if (QuantityText.Text == null)
            {
                val.Add(false);
            }

            foreach (bool c in val)
            {
                if (!c)
                {
                    return false;
                }
            }

            //check the formats
            val.Clear();

            val.Add(CheckDigits(UPCText.Text));

            // Verify Quantity is all Digits
            val.Add(CheckDigits(QuantityText.Text));

            // Verify Price follows a Valid format
            val.Add(CheckPrice(PriceText.Text));

            // Verify the Category is valid
            //val.Add(CheckCategory(itm.Category));

            foreach (bool c in val)
            {
                if (!c)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Check the string to see if all characters present are digits. Returns true if all characters are digits
        /// </summary>
        /// <param name="digits"></param> string from the information box to see if digits 
        /// <returns></returns> true if all characters are digits
        private bool CheckDigits(string digits)
        {
            char[] charArr = digits.ToCharArray();

            foreach (char c in charArr)
            {
                if (!Char.IsNumber(c))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// This method checks the string price to see if follows the format of a valid price. i.e. xxx.xx
        /// </summary>
        /// <param name="price"></param> is the string value of the price
        /// <returns></returns> true if the format is followed. false if not
        private bool CheckPrice(string price)
        {
            char[] charArr = price.ToCharArray();

            if (CheckDigits(price))
            {
                return true;
            }

            for (int pos = 0; pos < charArr.Length; pos++)
            {
                if (pos == (charArr.Length - 3))
                {
                    if (charArr[pos] != '.' && !Char.IsNumber(charArr[pos]))
                    {
                        return false;
                    }
                }
                else if (pos == (charArr.Length - 2))
                {
                    if (charArr[pos] != '.' && !Char.IsNumber(charArr[pos]))
                    {
                        return false;
                    }
                }
                else if (!Char.IsNumber(charArr[pos]))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// This method verifies the contents of category
        /// </summary>
        /// <param name="category">category is the category we are checking</param>
        /// <returns>true if category is valid</returns>
        private bool CheckCategory(Category category)
        {

            // what needs to be verified here??
            return true;
        }

        /// <summary>
        /// Check for barcode reader key combinations when textcomposition is entered
        /// </summary>
        /// <param name="sender">Object that triggered the event</param>
        /// <param name="e">Event details</param>
        private void Page_TextInput(object sender, TextCompositionEventArgs e)
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
                    ItemDTO lItem = InventoryListBox.Items
                                    .Cast<ItemDTO>()
                                    .Where(item => item.UPC.Equals(UPCText.Text)).FirstOrDefault();
                    if (lItem != null)
                    {
                        InventoryListBox.SelectedIndex = InventoryListBox.Items.IndexOf(lItem);
                    }
                    else
                    {
                        InventoryListBox.SelectedIndex = -1;
                        string upc = UPCText.Text;
                        UPCText.Text = "";
                        NameText.Text = "";
                        QuantityText.Text = "";
                        PriceText.Text = "";
                        CategoryListCombo.SelectedIndex = -1;
                        InventoryListBox.SelectedIndex = -1;
                        UPCText.Text = upc;
                    }
                }
                reading = false;
            }
        }
    }
}
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
    /// Interaction logic for Inventory_Page.xaml
    /// </summary>
    public partial class InventoryPage : Page
    {

            IInventoryDataAccess iDA = new InventoryDataAccess();
            //InventoryController inventoryControl = new InventoryController(iDA);
            InventoryController inventoryControl;
            public InventoryPage()
        {
                InitializeComponent();
                FillItemCombo();
                FillItemList();
                inventoryControl = new InventoryController(iDA);
            }

            /// <summary>
            /// This method populates the items scrollable list based on items in the inventory database.
            /// </summary>
            void FillItemList()
            {
                List<ItemDTO> itemList = inventoryControl.GetAllItems();

                foreach (ItemDTO item in itemList)
                {
                    InventoryListBox.Items.Add(item.UPC);
                }
            }

            /// <summary>
            /// This method populates the combo item list in the inventory database. 
            /// </summary>
            void FillItemCombo()
            {
                {
                    List<ItemDTO> itemList = inventoryControl.GetAllItems();

                    foreach (ItemDTO item in itemList)
                    {
                        InventoryListCombo.Items.Add(item.UPC);
                    }
                }
            }

            /// <summary>
            /// This method sets the information section with the data from the Item object
            /// </summary>
            private void InventoryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
            string text = InventoryListBox.SelectedItem.ToString();
            //string text = InventoryListBox.SelectedItem.ToString();
                ItemDTO itm = inventoryControl.GetItem(text);

                UPCText.Text = itm.UPC;
                NameText.Text = itm.Name;
                QuantityText.Text = itm.Quantity.ToString();
                PriceText.Text = itm.Price.ToString();
                CategoryText.Text = itm.Category.Name;
                IDText.Text = itm.ItemID.ToString();
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
                CategoryText.Text = itm.Category.Name;
                IDText.Text = itm.ItemID.ToString();
            }

            /// <summary>
            /// This method verifies that the UPC does not previously exists then creates a new item 
            /// with the item information.
            /// </summary>
            private void SaveBtn_Click(object sender, RoutedEventArgs e)
            {
                ItemDTO create = GetItemObj();
                Boolean created = inventoryControl.CreateItem(create);
                Boolean valid = Valid(create);

                if (created && valid)
                {
                    MessageBox.Show("Item was successfully created.");
                    ClearInputs();
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

                Boolean updated = inventoryControl.UpdateItem(uPC, update);
                Boolean valid = Valid(update);

                if (updated && valid)
                {
                    MessageBox.Show("Item was successfully updated.");
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("UPC is invalid. Please try again.");
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
                Boolean valid = CheckDigits(uPC);

                if (deleted && valid)
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
                Window.GetWindow(this).Close();
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
                CategoryText.Text = "";
                IDText.Text = "";
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
                Category category = new Category();
                category.Name = CategoryText.Text;

                string uPC = UPCText.Text;
                string name = NameText.Text;
                int quantity = int.Parse(QuantityText.Text);
                decimal price = decimal.Parse(PriceText.Text);
                string iDStr = IDText.Text;
                long iD = long.Parse(iDStr);

                itm.UPC = uPC;
                itm.ItemID = iD;
                itm.Name = name;
                itm.Quantity = quantity;
                itm.Price = price;
                itm.Category = category;

                return itm;
            }

            /// <summary>
            /// This method we validate the user input against specifications for neccessary item values.
            /// </summary>
            /// <param name="itm"></param> itm we are checking the parameters of
            /// <returns></returns> true if inputs for parameters are all good
            private bool Valid(ItemDTO itm)
            {
                List<bool> val = new List<bool>();

                // Verify UPC is all Digits
                val.Add(CheckDigits(itm.UPC));

                // Verify Quantity is all Digits
                val.Add(CheckDigits(QuantityText.Text));

                // Verify ItemID is all Digits
                val.Add(CheckDigits(IDText.Text));

                // Verify Price follows a Valid format
                val.Add(CheckPrice(PriceText.Text));

                // Verify the Category is valid
                val.Add(CheckCategory(itm.Category));

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

                for (int pos = 0; pos < charArr.Length; pos++)
                {
                    if (pos == (charArr.Length - 2))
                    {
                        if (charArr[pos] != '.')
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
            /// <param name="category"></param> category is the category we are checking
            /// <returns></returns> true if category is valid
            private Boolean CheckCategory(Category category)
            {

                // what needs to be verified here?? Is the ItemID the same as the CategoryID ?
                return true;
            }
        }
    }

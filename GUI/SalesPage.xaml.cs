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
        private bool reading = false;
        private List<ItemDTO> itemDTOs = new List<ItemDTO>();
        public SalesPage()
        {
            InitializeComponent();
            categoryControl = new CategoryController(new CategoryDataAccess());
            inventoryControl = new InventoryController(new InventoryDataAccess(new CategoryDataAccess()));
            cartController = new CartController();
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
            //this.NavigationService.Navigate(new MainMenu());
            this.NavigationService.GoBack();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

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

        }

        private void QuantityDown_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpCButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpC_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((TextBox)sender).FontSize = ((TextBox)sender).ActualHeight / 2;
        }

        private void PayTotal_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((TextBlock)sender).FontSize = ((TextBlock)sender).ActualHeight / 1.5;
        }

        /*private void Inventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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

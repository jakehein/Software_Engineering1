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
    /// Interaction logic for CategoryManager1.xaml
    /// </summary>
    public partial class CategoryManager : Page
    {
        private ICategoryController categoryControl => ControllerContainer.Instance.CategoryController;
        private IInventoryController inventoryControl => ControllerContainer.Instance.InventoryController;
        private readonly string uncatagorized = "Uncategorized";

        public CategoryManager()
        {
            InitializeComponent();
            FillCategoryListBox();
        }

        /// <summary>
        /// This method fills the CategoryListBox with all avaliable categories.
        /// </summary>
        private void FillCategoryListBox()
        {
            List<CategoryDTO> categories = categoryControl.GetAllCategories();
            categories.Sort();
            var sortedCategories = categories.OrderBy(x => x.Name);
            CategoryListBox.ItemsSource = sortedCategories;
        }

        /// <summary>
        /// This method takes the selected item in the CategoryListBox and places the item within the CategoryText.Text 
        /// </summary>
        /// <param name="sender">Object that triggered the event</param>
        /// <param name="e">Event details</param>
        private void CategoryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategoryDTO category = (CategoryDTO)CategoryListBox.SelectedItem;
            if (category != null)
            {
                CategoryText.Text = category.Name;
            }
            else
            {
                CategoryText.Text = "";
            }
        }

        /// <summary>
        /// This method saves new categories to the category list
        /// </summary>
        /// <param name="sender">Object that triggered the event</param>
        /// <param name="e">Event details</param>
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            CategoryDTO category = (CategoryDTO)CategoryListBox.SelectedItem;
            if (NameIsUnique(CategoryText.Text))
            {
                Category newCategory = new Category();
                newCategory.Name = CategoryText.Text;
                CategoryDTO newCategoryDTO = Category.createDTOFromCategory(newCategory);
                bool created = categoryControl.CreateCategory(newCategoryDTO);
                if (created)
                {
                    MessageBox.Show("Category Successfully Created");
                    CategoryText.Text = "";
                    FillCategoryListBox();
                    CategoryListBox.SelectedItem = null;
                }
            }
            else
            {
                MessageBox.Show("Please Create Unique Category");
            }
        }

        /// <summary>
        /// This method updates the category with the new text specified
        /// </summary>
        /// <param name="sender">Object that triggered the event</param>
        /// <param name="e">Event details</param>
        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((CategoryDTO)CategoryListBox.SelectedItem != null)
            {
                CategoryDTO category = (CategoryDTO)CategoryListBox.SelectedItem;
                if (category.Name != uncatagorized && NameIsUnique(CategoryText.Text))
                {
                    bool updated = categoryControl.UpdateCategory(category.CategoryID, CategoryText.Text);
                    if (updated)
                    {
                        MessageBox.Show("Category Successfully Updated");
                    }
                    CategoryText.Text = "";
                    FillCategoryListBox();
                    CategoryListBox.SelectedItem = null;
                }
                else if (category.Name == uncatagorized)
                {
                    MessageBox.Show("Cannot Update Uncatagorized. Please try again.");
                }
                else
                {
                    MessageBox.Show("Name is not valid. Please try again.");
                }
            }
            else
            {
                MessageBox.Show("Please select category to update.");
            }
        }

        /// <summary>
        /// This method allows for the deletion of categories
        /// Categories containing items will be reassigned a category of 'Uncatagorized'
        /// 'Uncatagorized' may not be deleted
        /// </summary>
        /// <param name="sender">Object that triggered the event</param>
        /// <param name="e">Event details</param>
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            CategoryDTO category = (CategoryDTO)CategoryListBox.SelectedItem; // this is null
            CategoryText.Text = "";

            if (category.Name != uncatagorized)
            {
                ItemsSetToUnclassified(category);
                bool deleted = categoryControl.DeleteCategory(category);
                if (deleted)
                {
                    MessageBox.Show("Category has been deleted");
                    FillCategoryListBox();
                }
                else
                {
                    MessageBox.Show("Category was unable to be deleted");
                }
            }
            else
            {
                MessageBox.Show(uncatagorized + " is not avaliable to delete");
            }
        }

        /// <summary>
        /// This method verifes that the typed category is unique in comparison to all other categories
        /// </summary>
        /// <param name="name"> string name of category we are comparing</param>
        /// <returns>true if name is unique from all other categories</returns>
        private bool NameIsUnique(string name)
        {
            List<CategoryDTO> categories = categoryControl.GetAllCategories();

            foreach (CategoryDTO category in categories)
            {
                if (category.Name == name)
                {
                    return false;
                }
            }
            if (name == "")
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// This method sets all items within the deleted category to 'unclassified'
        /// </summary>
        /// <param name="category">DTO object of the category getting deleted</param>
        /// <returns> true if all items in the specified category are successfully set to unclassified</returns>
        private bool ItemsSetToUnclassified(CategoryDTO category)
        {
            List<ItemDTO> itemsList = inventoryControl.GetAllItemsFromCategory(category.CategoryID);
            CategoryDTO unclassifiedCategory = categoryControl.GetCategoryByName(uncatagorized); // we are getting a null category that is passed
            foreach (ItemDTO item in itemsList)
            {
                bool updated = inventoryControl.UpdateItemCategory(item, unclassifiedCategory);
                if (!updated)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Navigates the user from the Category Manager to the Inventory Page
        /// </summary>
        /// <param name="sender">Object that triggered the event</param>
        /// <param name="e">Event details</param>
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new InventoryPage());
        }

        /// <summary>
        /// Resize the category font on window size change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResizeCategoryFontSize(object sender, SizeChangedEventArgs e)
        {
            ((TextBox)sender).FontSize = ((TextBox)sender).ActualHeight * .7;
        }
    }
}

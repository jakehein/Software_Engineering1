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
    /// Interaction logic for CategoryManager1.xaml
    /// </summary>
    public partial class CategoryManager : Page
    {
        private ICategoryController categoryControl;
        private IInventoryController inventoryControl;
        private readonly string uncatagorized = "Uncategorized";

        public CategoryManager()
        {
            InitializeComponent();
            categoryControl = new CategoryController(new CategoryDataAccess());
            inventoryControl = new InventoryController(new InventoryDataAccess(new CategoryDataAccess()));
            FillCategoryListBox();
        }

        /// <summary>
        /// This method fills the CategoryListBox with all avaliable categories
        /// </summary>
        private void FillCategoryListBox()
        {
            List<CategoryDTO> categories = categoryControl.GetAllCategories();
            categories.Sort();
            var sortedCategories = categories.OrderBy(x => x.Name);
            CategoryListBox.ItemsSource = sortedCategories;
        }

        /// <summary>
        /// This method takes the selection and places the item within the category box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            CategoryDTO category = (CategoryDTO)CategoryListBox.SelectedItem;
            if (nameIsUnique(CategoryText.Text))
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
                }
            }
            else
            {
                MessageBox.Show("Please Create Unique Category");
            }
        }

        /// <summary>
        /// This method updates the category with the new text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            CategoryDTO category = (CategoryDTO)CategoryListBox.SelectedItem;
            if (category.Name != uncatagorized && nameIsUnique(CategoryText.Text))
            {
                bool updated = categoryControl.UpdateCategory(category.CategoryID, CategoryText.Text);
                if (updated)
                {
                    MessageBox.Show("Category Successfully Updated");
                }
                CategoryText.Text = "";
                FillCategoryListBox();
            }
            else
            {
                MessageBox.Show("Name is not unique. Please try again.");
            }
        }

        /// <summary>
        /// This method allows for the deletion of categories
        /// Categories containing items will be reassigned a category of 'Uncatagorized'
        /// 'Uncatagorized' may not be deleted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            CategoryDTO category = (CategoryDTO)CategoryListBox.SelectedItem; // this is null
            CategoryText.Text = "";

            if (category.Name != uncatagorized)
            {
                itemsSetToUnclassified(category);
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
        /// This method verifes that the typed category is unique in comparrison to all other categories
        /// </summary>
        /// <param name="name"> string name of category we are comparing</param>
        /// <returns>true if unique</returns>
        private bool nameIsUnique(string name)
        {
            List<CategoryDTO> categories = categoryControl.GetAllCategories();

            foreach (CategoryDTO category in categories)
            {
                if (category.Name == name)
                {
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// This method sets all items within the deleted category to 'unclassified'
        /// </summary>
        /// <param name="category">DTO object of the category getting deleted</param>
        /// <returns></returns>
        private bool itemsSetToUnclassified(CategoryDTO category)
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

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new InventoryPage());
        }
    }
}

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
using System.Windows.Shapes;

namespace FinalProject1
{
    /// <summary>
    /// Interaction logic for CategoryManager.xaml
    /// </summary>
    public partial class CategoryManager : Window
    {
        private ICategoryController categoryControl;
        private readonly string uncatagorized = "Uncatagorized";
        public CategoryManager()
        {
            InitializeComponent();
            categoryControl = new CategoryController(new CategoryDataAccess());
            FillCategoryListBox();
        }

        /// <summary>
        /// This method fills the CategoryListBox with all avaliable categories
        /// </summary>
        private void FillCategoryListBox()
        {
            List<CategoryDTO> categories = categoryControl.GetAllCategories();
            CategoryListBox.ItemsSource = categories;
        }

        /// <summary>
        /// This method takes the selection and places the item within the category box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategoryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategoryDTO category = (CategoryDTO)CategoryListBox.SelectedItem;
            CategoryText.Text = category.Name;
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
                }
                FillCategoryListBox();
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
            CategoryDTO category = (CategoryDTO)CategoryListBox.SelectedItem;
            if (category.Name != uncatagorized)
            {

            }
        }

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
    }
}

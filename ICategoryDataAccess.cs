using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    interface ICategoryDataAccess
    {
        bool CreateCategory(Category category);
        Category GetCategory(long categoryID);
        List<Category> GetCategories();
    }
}

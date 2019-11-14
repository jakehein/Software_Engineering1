using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    interface ICategoryDataAccess
    {
        bool CreateCategory(CategoryDTO category);

        bool UpdateCategory(long iD, string category);
        CategoryDTO GetCategory(long categoryID);
        List<CategoryDTO> GetCategories();
    }
}

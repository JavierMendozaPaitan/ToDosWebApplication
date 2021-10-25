using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDos.Models.ViewModels;

namespace ToDos.Abstractions.Actions
{
    public interface ICollectionActions
    {
        CategoriesViewModel GetCategoriesFromCollection();
        CategoriesViewModel GetCategoriesWithSelected(string category);
        CategoriesViewModel GetCategoriesWithToDosFiltered(string category, string search);
        void DeleteCategoryById(Guid id);
        void DeleteToDoById(Guid id);
    }
}

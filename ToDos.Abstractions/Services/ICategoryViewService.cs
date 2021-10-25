using DataProvider.Models.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDos.Models.ViewModels;

namespace ToDos.Abstractions.Services
{
    public interface ICategoryViewService
    {
        Func<CategoriesViewModel> GetCategories { get; }
        Func<string, CategoriesViewModel> GetCategoriesSelected { get; }
        Func<string, string, CategoriesViewModel> GetCategoriesFiltered { get; }
        Func<Guid, Category> GetCategory { get; }
        Action<Guid> DeleteCategory { get; }
        Action<Category> CreateCategory { get; }
        Action<Category> EditCategory { get; }
    }
}

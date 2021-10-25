using DataProvider.Abstractions.Interfaces.Services;
using DataProvider.Models.MongoDb;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDos.Abstractions.Actions;
using ToDos.Abstractions.Services;
using ToDos.Models.ViewModels;

namespace ToDos.Engines.Services
{
    public class CategoryViewService : ICategoryViewService
    {
        private readonly ICategoryCollectionService _collection;
        private readonly ICollectionActions _actions;
        public CategoryViewService
            (
            ICategoryCollectionService collection,
            ICollectionActions actions
            )
        {
            _collection = collection;
            _actions = actions;
        }
        public Func<CategoriesViewModel> GetCategories => _actions.GetCategoriesFromCollection;
        public Func<Guid, Category> GetCategory => (id) => _collection.SelectById(id.ToString());
        public Action<Guid> DeleteCategory => _actions.DeleteCategoryById;
        public Action<Category> CreateCategory => _collection.Insert;
        public Action<Category> EditCategory => _collection.Update;
        public Func<string, CategoriesViewModel> GetCategoriesSelected => _actions.GetCategoriesWithSelected;
        public Func<string, string, CategoriesViewModel> GetCategoriesFiltered => _actions.GetCategoriesWithToDosFiltered;


    }
}

using DataProvider.Abstractions.Interfaces.Services;
using DataProvider.Models.MongoDb;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDos.Abstractions.Actions;
using ToDos.Models.ViewModels;

namespace ToDos.Engines.Actions
{
    public class CollectionActions:ICollectionActions
    {
        private readonly ICategoryCollectionService _collection;
        private readonly IToDoCollectionService _toDoCollection;
        private readonly ILogger<CollectionActions> _logger;
        public CollectionActions
            (
            ICategoryCollectionService collection,
            IToDoCollectionService toDoCollection,
            ILogger<CollectionActions> logger
            )
        {
            _collection = collection;
            _toDoCollection = toDoCollection;
            _logger = logger;
        }

        public void DeleteCategoryById(Guid id)
        {
            try
            {
                var category = new Category
                {
                    Id = id.ToString()
                };
                _collection.Delete(category);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Problems deleting category {id}: {ex.StackTrace}");
                throw;
            }
        }

        public void DeleteToDoById(Guid id)
        {
            try
            {
                var toDo = new ToDo
                {
                    Id = id.ToString()
                };
                _toDoCollection.Delete(toDo);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Problems deleting ToDo {id}: {ex.StackTrace}");
                throw;
            }
        }

        public CategoriesViewModel GetCategoriesFromCollection()
        {
            try
            {
                var viewModel = new CategoriesViewModel
                {
                    Categories = _collection.SelectAll()
                };
                var firstCategory = viewModel.Categories.FirstOrDefault().Name;
                var toDos = _toDoCollection.SelectByCategory(firstCategory);
                viewModel.SelectedCategory = firstCategory;
                viewModel.ToDos.Add(firstCategory, toDos);

                return viewModel;
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Problems getting categories: {ex.StackTrace}");
                throw;
            }
        }

        public CategoriesViewModel GetCategoriesWithSelected(string category)
        {
            try
            {
                if (string.IsNullOrEmpty(category)) throw new ArgumentNullException();

                var viewModel = new CategoriesViewModel
                {
                    Categories = _collection.SelectAll()
                };
                var toDos = _toDoCollection.SelectByCategory(category);
                viewModel.SelectedCategory = category;
                viewModel.ToDos.Add(category, toDos);

                return viewModel;
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Problems getting categories: {ex.StackTrace}");
                throw;
            }
        }

        public CategoriesViewModel GetCategoriesWithToDosFiltered(string category, string search)
        {
            try
            {
                if (string.IsNullOrEmpty(category)) throw new ArgumentNullException();

                var viewModel = GetCategoriesWithSelected(category);

                if (string.IsNullOrEmpty(search)) return viewModel;

                viewModel.ToDos.Values.FirstOrDefault().RemoveAll(x => !x.Activity.Contains(search, StringComparison.OrdinalIgnoreCase));

                return viewModel;
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Problems getting categories: {ex.StackTrace}");
                throw;
            }
        }
    }
}

using DataProvider.Abstractions.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using Microsoft.Extensions.Logging;
using ToDos.Engines.Actions;
using ToDos.Models.ViewModels;
using DataProvider.Models.MongoDb;
using ToDos.Abstractions.Actions;
using ToDos.Engines.Services;
using ToDos.Abstractions.Services;

namespace ToDos.Tests.Services
{
    public class CategoryViewServiceTests
    {
        private Mock<ICategoryCollectionService> categoryService;
        private Mock<ICollectionActions> actions;
        private Mock<Func<string, CategoriesViewModel>> mockGetCategoriesSelected;
        private Mock<Func<Guid, Category>> mockGetCategory;
        private Mock<Func<CategoriesViewModel>> mockGetCategories;

        private CategoryViewService service;

        [SetUp]
        public void Setup()
        {
            categoryService = new Mock<ICategoryCollectionService>();
            actions = new Mock<ICollectionActions>();
            mockGetCategory = new Mock<Func<Guid, Category>>();
            mockGetCategories = new Mock<Func<CategoriesViewModel>>();
            mockGetCategoriesSelected = new Mock<Func<string, CategoriesViewModel>>();
            service = new CategoryViewService(categoryService.Object, actions.Object);
        }

        [Test]
        public void GetCategories_Success()
        {
            var categories = new CategoriesViewModel
            {
                Categories = new List<Category>(),
                SelectedCategory = string.Empty,
                ToDos = new Dictionary<string, List<ToDo>>()
            };
            mockGetCategories.Setup(x => x.Invoke()).Returns(categories);
            var result = mockGetCategories.Object.Invoke();
            Assert.AreEqual(categories, result);
        }

        [Test]
        public void GetCategory_Success()
        {
            var category = new Category
            {
                Id = string.Empty
            };
            mockGetCategory.Setup(x => x.Invoke(It.IsAny<Guid>())).Returns(category);
            var result = mockGetCategory.Object.Invoke(It.IsAny<Guid>());
            Assert.AreEqual(category, result);
        }

        [Test]
        public void GetCategoriesSelected_Success()
        {
            var categories = new CategoriesViewModel
            {
                Categories = new List<Category>(),
                SelectedCategory = string.Empty,
                ToDos = new Dictionary<string, List<ToDo>>()
            };
            mockGetCategoriesSelected.Setup(x => x.Invoke(It.IsAny<string>())).Returns(categories);
            var result = mockGetCategoriesSelected.Object.Invoke(It.IsAny<string>());
            Assert.AreEqual(categories, result);
        }
    }
}

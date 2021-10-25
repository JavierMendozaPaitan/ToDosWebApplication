using DataProvider.Abstractions.Interfaces.Services;
using DataProvider.Models.MongoDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDos.DataProvider.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryCollectionService _collection;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController
            (
            ICategoryCollectionService collection,
            ILogger<CategoryController> logger
            )
        {
            _collection = collection;
            _logger = logger;
        }
                
        [HttpGet]
        public List<Category> GetCategories()
        {
            try
            {
                var list = _collection.SelectAll();

                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot Get Categories: {ex.StackTrace}");
                throw;
            }
        }

        [HttpPost]
        public void AddCategory([FromForm] Category category)
        {
            try
            {
                _collection.Insert(category);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot Add Category: {ex.StackTrace}");
                throw;
            }
        }

        [HttpPost("Update")]
        public void UpdateCategory([FromForm] Category category)
        {
            try
            {
                _collection.Update(category);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot Update Category: {ex.StackTrace}");
                throw;
            }
        }

        [HttpPost("Remove")]
        public void RemoveCategory([FromForm] Category category)
        {
            try
            {
                _collection.Delete(category);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot Remove Category: {ex.StackTrace}");
                throw;
            }
        }
    }
}

using DataProvider.Models.MongoDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ToDos.Abstractions.Services;
using ToDos.Models.ViewModels;
using ToDosWebApp.Models;

namespace ToDosWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryViewService _category;

        public HomeController(
            ICategoryViewService category,
            ILogger<HomeController> logger
            )
        {
            _category = category;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                var categories = _category.GetCategories();

                return View(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Problems rendering Index: {ex.StackTrace}");
                throw;
            }            
        }

        public IActionResult IndexSelected(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name)) return NotFound();

                var categories = _category.GetCategoriesSelected(name);

                return View(nameof(Index), categories);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Problems rendering Index: {ex.StackTrace}");
                throw;
            }
        }

        public IActionResult IndexFiltered(string category, string search)
        {
            try
            {
                if (string.IsNullOrEmpty(category)) return NotFound();

                var categories = _category.GetCategoriesFiltered(category, search);

                return View(nameof(Index), categories);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Problems rendering Index: {ex.StackTrace}");
                throw;
            }
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _category.CreateCategory(category);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Problems creating category: {ex.StackTrace}");
                throw;
            }
        }        

        public IActionResult EditCategory(Guid id)
        {
            try
            {
                if (id.Equals(Guid.Empty)) return NotFound();

                var category = _category.GetCategory(id);

                if (category == null) return NotFound();

                return View(category);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Problems editing category: {ex.StackTrace}");
                throw;
            }            
        }

        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _category.EditCategory(category);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Problems editing category: {ex.StackTrace}");
                throw;
            }
        }       

        public IActionResult DeleteCategory(Guid id)
        {
            try
            {
                if (id.Equals(Guid.Empty)) return NotFound();

                _category.DeleteCategory(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Problems deleting category: {ex.StackTrace}");
                throw;
            }            
        }

        [HttpPost]
        public IActionResult Cancel()
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Problems cancelling: {ex.StackTrace}");
                throw;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

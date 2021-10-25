using DataProvider.Models.MongoDb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDos.Abstractions.Services;

namespace ToDosWebApp.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ILogger<ToDoController> _logger;
        private readonly IToDoViewService _toDo;

        public ToDoController(
            IToDoViewService toDo,
            ILogger<ToDoController> logger
            )
        {
            _toDo = toDo;
            _logger = logger;
        }

        public IActionResult CreateToDo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateToDo(ToDo toDo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _toDo.CreateToDo(toDo);
                }

                return RedirectToAction(nameof(Index), "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Problems creating ToDo: {ex.StackTrace}");
                throw;
            }
        }

        public IActionResult EditToDo(Guid id)
        {
            try
            {
                if (id.Equals(Guid.Empty)) return NotFound();

                var toDo = _toDo.GetToDo(id);

                if (toDo == null) return NotFound();

                return View(toDo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Problems editing ToDo: {ex.StackTrace}");
                throw;
            }
        }

        [HttpPost]
        public IActionResult EditToDo(ToDo toDo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _toDo.EditToDo(toDo);
                }

                return RedirectToAction(nameof(Index), "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Problems editing ToDo: {ex.StackTrace}");
                throw;
            }
        }

        public IActionResult DeleteToDo(Guid id)
        {
            try
            {
                if (id.Equals(Guid.Empty)) return NotFound();

                _toDo.DeleteToDo(id);

                return RedirectToAction(nameof(Index), "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Problems deleting ToDo: {ex.StackTrace}");
                throw;
            }
        }

        [HttpPost]
        public IActionResult Cancel()
        {
            try
            {
                return RedirectToAction(nameof(Index), "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Problems cancelling: {ex.StackTrace}");
                throw;
            }
        }
    }
}

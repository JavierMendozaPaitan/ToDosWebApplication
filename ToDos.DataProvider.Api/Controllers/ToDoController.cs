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
    public class ToDoController : ControllerBase
    {
        private readonly IToDoCollectionService _collection;
        private readonly ILogger<ToDoController> _logger;
        public ToDoController
            (
            IToDoCollectionService collection,
            ILogger<ToDoController> logger
            )
        {
            _collection = collection;
            _logger = logger;
        }


        [HttpGet]
        public List<ToDo> GetToDos()
        {
            try
            {
                var list = _collection.SelectAll();

                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot Get ToDos: {ex.StackTrace}");
                throw;
            }
        }

        [HttpPost]
        public void AddToDo([FromForm] ToDo todo)
        {
            try
            {
                _collection.Insert(todo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot Add ToDo: {ex.StackTrace}");
                throw;
            }
        }

        [HttpPost("Update")]
        public void UpdateCategory([FromForm] ToDo todo)
        {
            try
            {
                _collection.Update(todo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot Update ToDo: {ex.StackTrace}");
                throw;
            }
        }

        [HttpPost("Remove")]
        public void RemoveCategory([FromForm] ToDo todo)
        {
            try
            {
                _collection.Delete(todo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot Remove ToDo: {ex.StackTrace}");
                throw;
            }
        }

    }
}

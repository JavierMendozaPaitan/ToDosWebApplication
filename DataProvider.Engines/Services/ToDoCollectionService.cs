using DataProvider.Abstractions.Interfaces.MongoDb;
using DataProvider.Abstractions.Interfaces.Services;
using DataProvider.Models.Enums;
using DataProvider.Models.MongoDb;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Engines.Services
{
    public class ToDoCollectionService : IToDoCollectionService
    {
        private readonly IMongoCollectionObject<ToDo> _collection;
        private readonly ILogger _logger;
        public ToDoCollectionService
            (
            IMongoCollectionObject<ToDo> collection,
            ILogger<IMongoCollectionObject<ToDo>> logger
            )
        {
            _collection = collection;
            _logger = logger;
        }

        public Action<ToDo> Insert => InsertToDo;
        public Action<ToDo> Update => _collection.Update;
        public Action<ToDo> Delete => _collection.Remove;
        public Func<string, ToDo> SelectById => _collection.SelectById;
        public Func<List<ToDo>> SelectAll => _collection.SelectAll;
        public Func<string, List<ToDo>> SelectByCategory => SelectToDosByCategory;

        private void InsertToDo(ToDo todo)
        {
            todo.Status = todo.Status != ToDoStatus.Initiated ? todo.Status : ToDoStatus.Initiated;
            todo.Priority = todo.Priority != ToDoPriority.Low ? todo.Priority : ToDoPriority.Low;
            _collection.Insert(todo);
        }

        private List<ToDo> SelectToDosByCategory(string category)
        {
            try
            {
                var list = _collection.Collection.Find(x => x.Category.Equals(category)).ToList();

                return list;
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Problems getting ToDos: {ex.StackTrace}");
                throw;
            }
        }
    }
}

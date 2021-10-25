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

namespace ToDos.Engines.Services
{
    public class ToDoViewService : IToDoViewService
    {        
        private readonly IToDoCollectionService _collection;
        private readonly ICollectionActions _actions;
        public ToDoViewService
            (            
            IToDoCollectionService collection,
            ICollectionActions actions
            )
        {            
            _collection = collection;
            _actions = actions;
        }

        public Func<Guid, ToDo> GetToDo => (id) => _collection.SelectById(id.ToString());
        public Action<Guid> DeleteToDo => _actions.DeleteToDoById;
        public Action<ToDo> CreateToDo => _collection.Insert;
        public Action<ToDo> EditToDo => _collection.Update;

    }
}

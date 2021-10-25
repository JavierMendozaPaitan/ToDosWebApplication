using DataProvider.Models.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDos.Abstractions.Services
{
    public interface IToDoViewService
    {
        Func<Guid, ToDo> GetToDo { get; }
        Action<Guid> DeleteToDo { get; }
        Action<ToDo> CreateToDo { get; }
        Action<ToDo> EditToDo { get; }
    }
}

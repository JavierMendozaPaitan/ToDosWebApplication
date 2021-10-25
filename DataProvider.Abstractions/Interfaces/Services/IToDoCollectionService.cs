using DataProvider.Models.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Abstractions.Interfaces.Services
{
    public interface IToDoCollectionService
    {
        Action<ToDo> Insert { get; }
        Action<ToDo> Update { get; }
        Action<ToDo> Delete { get; }
        Func<string, ToDo> SelectById { get; }
        Func<string, List<ToDo>> SelectByCategory { get; }
        Func<List<ToDo>> SelectAll { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider.Models.MongoDb;

namespace DataProvider.Abstractions.Interfaces.Services
{
    public interface ICategoryCollectionService
    {
        Action<Category> Insert { get; }
        Action<Category> Update { get; }
        Action<Category> Delete { get; }
        Func<string, Category> SelectById { get; }
        Func<List<Category>> SelectAll { get; }
    }
}

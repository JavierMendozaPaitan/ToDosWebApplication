using DataProvider.Abstractions.Interfaces.MongoDb;
using DataProvider.Abstractions.Interfaces.Services;
using DataProvider.Models.MongoDb;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Engines.Services
{
    public class CategoryCollectionService : ICategoryCollectionService
    {
        private readonly IMongoCollectionObject<Category> _collection;
        private readonly ILogger _logger;
        public CategoryCollectionService
            (
            IMongoCollectionObject<Category> collection,
            ILogger<IMongoCollectionObject<Category>> logger
            )
        {
            _collection = collection;
            _logger = logger;
        }
        public Action<Category> Insert => _collection.Insert;
        public Action<Category> Update => _collection.Update;
        public Action<Category> Delete => _collection.Remove;
        public Func<string, Category> SelectById => _collection.SelectById;
        public Func<List<Category>> SelectAll => _collection.SelectAll;
        
    }
}

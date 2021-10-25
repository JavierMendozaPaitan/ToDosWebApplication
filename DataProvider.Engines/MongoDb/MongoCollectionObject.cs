using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider.Abstractions.Interfaces.MongoDb;
using DataProvider.Model.Abstractions;

namespace DataProvider.Engines.MongoDb
{
    public class MongoCollectionObject<T> : IMongoCollectionObject<T>
    {
        private readonly IMongoDatabaseObject _database;
        private readonly ILogger _logger;
        private readonly IMongoCollection<T> _collection;
        public MongoCollectionObject
            (
            IMongoDatabaseObject database,
            ILogger<MongoCollectionObject<T>> logger
            )
        {
            try
            {
                _database = database;
                _logger = logger;
                _collection = _database.Database.GetCollection<T>(typeof(T).Name);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Problems generating MongoDb Collection Object: {ex.StackTrace}");
                throw;
            }
        }
        public string Name => throw new NotImplementedException();

        public IMongoCollection<T> Collection => _collection;

        public IMongoDatabaseObject Database => _database;

        public void Insert(T obj)
        {
            try
            {
                var today = DateTime.Now;
                ((ICollection)obj).CreatedDate = today;
                ((ICollection)obj).ModifiedDate = today;
                ((ICollection)obj).Id = Guid.NewGuid().ToString();
                _collection.InsertOne(obj);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Problems inserting: {ex.StackTrace}");
                throw;
            }
        }

        public void Remove(T obj)
        {
            try
            {
                _collection.DeleteOne<T>(x => ((ICollection)x).Id == ((ICollection)obj).Id);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Problems removing: {ex.StackTrace}");
                throw;
            }
        }

        public T SelectById(string id)
        {
            try
            {
                var obj = _collection.Find(x => ((ICollection)x).Id == id).FirstOrDefault();

                return obj;
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Problems selecting by Id (string): {ex.StackTrace}");
                throw;
            }
        }

        public void Update(T obj)
        {
            try
            {
                T dbobj = SelectById(((ICollection)obj).Id);
                ((ICollection)obj).CreatedDate = ((ICollection)dbobj).CreatedDate;
                ((ICollection)obj).ModifiedDate = DateTime.Now;
                _collection.ReplaceOne(x => ((ICollection)x).Id == ((ICollection)obj).Id, obj);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Problems updating: {ex.StackTrace}");
                throw;
            }
        }
        public List<T> SelectAll()
        {
            try
            {
                var list = _collection.Find(new BsonDocument()).ToList();

                return list;
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Problems selecting all: {ex.StackTrace}");
                throw;
            }
        }
    }
}

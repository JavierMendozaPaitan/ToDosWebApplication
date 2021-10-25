using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Abstractions.Interfaces.MongoDb
{
    public interface IMongoCollectionObject<T>
    {
        string Name { get; }
        IMongoCollection<T> Collection { get; }
        IMongoDatabaseObject Database { get; }
        T SelectById(string id);
        void Insert(T obj);
        void Update(T obj);
        void Remove(T obj);
        List<T> SelectAll();
    }
}

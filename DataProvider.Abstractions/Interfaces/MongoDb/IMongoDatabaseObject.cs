using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Abstractions.Interfaces.MongoDb
{
    public interface IMongoDatabaseObject
    {
        string Name { get; }
        IMongoClient Client { get; }
        IMongoDatabase Database { get; }
    }
}

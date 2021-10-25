using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider.Abstractions.Interfaces.Configuration;
using DataProvider.Abstractions.Interfaces.MongoDb;

namespace DataProvider.Engines.MongoDb
{
    public class MongoDatabaseObject : IMongoDatabaseObject
    {
        private readonly IDataProviderConfiguration _config;
        private readonly ILogger<MongoDatabaseObject> _logger;
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        public MongoDatabaseObject
            (
            IDataProviderConfiguration config,
            ILogger<MongoDatabaseObject> logger
            )
        {
            try
            {
                _config = config;
                _logger = logger;
                var settings = _config.SysDbConfig.ToDoDatabase.Settings;
                var connectionString = $@"{settings.Protocol}://TestingUser:hrmtaohBmqVJ0TSJ@{settings.Url}/{Name}?{settings.Query}";
                _client = new MongoClient(connectionString);
                _database = _client.GetDatabase(Name);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Problems generating MongoDb Database Object: {ex.StackTrace}");
                throw;
            }            
        }
        public string Name => _config.SysDbConfig.ToDoDatabase.Name;
        public IMongoClient Client => _client;
        public IMongoDatabase Database => _database;
    }
}

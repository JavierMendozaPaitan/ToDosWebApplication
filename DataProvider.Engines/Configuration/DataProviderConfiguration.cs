using DataProvider.Abstractions.Interfaces.Actions;
using DataProvider.Abstractions.Interfaces.Configuration;
using DataProvider.Config;
using DataProvider.Config.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Engines.Configuration
{
    public class DataProviderConfiguration : IDataProviderConfiguration
    {
        private readonly IConfigOptions _config;
        public DataProviderConfiguration(IConfigOptions config)
        {
            _config = config;
        }

        public SysDatabaseConfig SysDbConfig => _config.GetConfiguration<SysDatabaseConfig>();
    }
}

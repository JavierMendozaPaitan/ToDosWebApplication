using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using DataProvider.Abstractions.Interfaces.Actions;

namespace DataProvider.Engines.Actions
{
    public class ConfigOptions : IConfigOptions
    {
        private readonly IConfiguration _config;
        private readonly ILogger<ConfigOptions> _logger;
        public ConfigOptions(IConfiguration config, ILogger<ConfigOptions> logger)
        {
            _config = config;
            _logger = logger;
        }

        public T GetConfiguration<T>() 
        {
            try
            { 
                T configOptions = _config.GetSection(typeof(T).Name).Get<T>();

                return configOptions;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Problems getting configuration", ex.StackTrace);
                throw;
            }
        }
    }
}

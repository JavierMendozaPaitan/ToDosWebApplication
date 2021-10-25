using DataProvider.Config;
using DataProvider.Config.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Abstractions.Interfaces.Configuration
{
    public interface IDataProviderConfiguration
    {        
        SysDatabaseConfig SysDbConfig { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Config.Models
{
    public class DatabaseConfig
    {
        public string Name { get; set; }
        public ConnectionSettingsConfig Settings { get; set; }
    }
}

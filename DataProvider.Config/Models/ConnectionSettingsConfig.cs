using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Config.Models
{
    public class ConnectionSettingsConfig
    {
        public string Url { get; set; }
        public int Port { get; set; }
        public string ConnectionString { get; set; }
        public string Protocol { get; set; }
        public string Query { get; set; }
    }
}

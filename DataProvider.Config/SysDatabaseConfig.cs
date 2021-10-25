using DataProvider.Config.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Config
{
    public class SysDatabaseConfig
    {
        public DatabaseConfig ToDoDatabase { get; set; }
        public CollectionConfig Category { get; set; }
        public CollectionConfig ToDo { get; set; }
    }
}

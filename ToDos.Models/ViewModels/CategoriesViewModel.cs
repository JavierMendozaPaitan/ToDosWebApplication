using DataProvider.Models.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDos.Models.ViewModels
{
    public class CategoriesViewModel
    {
        public CategoriesViewModel()
        {
            Categories = new List<Category>();
            ToDos = new Dictionary<string, List<ToDo>>();
        }
        public List<Category> Categories { get; set; }
        public Dictionary<string, List<ToDo>> ToDos { get; set; }
        public string SelectedCategory { get; set; }

    }
}

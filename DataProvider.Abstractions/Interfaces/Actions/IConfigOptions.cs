using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Abstractions.Interfaces.Actions
{
    public interface IConfigOptions
    {
        T GetConfiguration<T>();
    }
}

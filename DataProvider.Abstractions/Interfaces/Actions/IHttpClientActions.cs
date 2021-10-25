using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Abstractions.Interfaces.Actions
{
    public interface IHttpClientActions
    {
        IHttpClientFactory ClientFactory { get; }
        HttpClient Client { get; }
        Task<string> ContentAsString(string url);
        Task<U> ContentAsType<U>(string url);
    }
}

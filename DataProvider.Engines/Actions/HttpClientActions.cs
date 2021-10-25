using DataProvider.Abstractions.Interfaces.Actions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Engines.Actions
{
    public class HttpClientActions : IHttpClientActions
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly HttpClient _client;
        private readonly IJsonSerialization _json;
        private readonly ILogger _logger;
        public HttpClientActions
            (
            IHttpClientFactory clientFactory,
            IJsonSerialization json,
            ILogger<HttpClientActions> logger
            )
        {
            try
            {
                _clientFactory = clientFactory;
                _client = _clientFactory.CreateClient();
                _json = json;
                _logger = logger;
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Problems creating HttpClient: {ex.StackTrace}");
                throw;
            }
        }

        public IHttpClientFactory ClientFactory => _clientFactory;
        public HttpClient Client => _client;

        public async Task<string> ContentAsString(string url)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await _client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var str = await response.Content.ReadAsStringAsync();

                return str;
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Problems getting response content as string: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<U> ContentAsType<U>(string url)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await _client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var strm = await response.Content.ReadAsStreamAsync();
                U obj = await _json.Deserialize<U>(strm);
                //var str = await response.Content.ReadAsStringAsync();
                //U obj = _json.Deserialize<U>(str);

                return obj;
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Problems getting response content as type {nameof(U)}: {ex.StackTrace}");
                throw;
            }
        }
    }
}

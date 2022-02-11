using System;
using System.Net.Http;
using System.Threading.Tasks;
using EndpointHandler.Domain.Interfaces.Services;

namespace EndpointHandler.Infrastructure
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpointUri;

        public HttpService(HttpClient httpClient, string endpointUri)
        {
            _httpClient = httpClient;

            if (!CheckIfEndpointUriCorrect(endpointUri)) throw
                new ArgumentException("Endpoint URI can not be empty or null.");

            _endpointUri = endpointUri;
        }
        private static bool CheckIfEndpointUriCorrect(string endpointUri)
            => !string.IsNullOrEmpty(endpointUri);

        private Task<string> GetResponseAsync()
            =>  _httpClient.GetStringAsync(_endpointUri);

        public async Task<string> TryGetResponseOrTimeout()
        {
            int timeoutTime = 4000;
            Task<string> task = GetResponseAsync();

            if (await Task.WhenAny(task, Task.Delay(timeoutTime)) != task)
                throw new TimeoutException($"Response timeout reached, {timeoutTime} ms.");

            return task.Result;
        }
    }
}
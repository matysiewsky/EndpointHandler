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

        public Task GetResponseAsync()
            => _httpClient.GetAsync(_endpointUri);
    }
}
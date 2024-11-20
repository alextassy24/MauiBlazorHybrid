using System.Net.Http.Json;
using BlazorHybrid.Interfaces.Services;

namespace BlazorHybrid.Services
{
    public class HttpService(HttpClient httpClient) : IHttpService
    {
        private readonly HttpClient _httpClient =
            httpClient ?? throw new ArgumentNullException(nameof(httpClient));

        public async Task<T> GetAsync<T>(string uri)
        {
            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task PostAsync<T>(string uri, T content)
        {
            var response = await _httpClient.PostAsJsonAsync(uri, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task PutAsync<T>(string uri, T content)
        {
            var response = await _httpClient.PutAsJsonAsync(uri, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(string uri)
        {
            var response = await _httpClient.DeleteAsync(uri);
            response.EnsureSuccessStatusCode();
        }
    }
}

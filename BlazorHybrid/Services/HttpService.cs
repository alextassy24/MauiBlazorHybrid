using System.Net.Http.Headers;
using System.Net.Http.Json;
using BlazorHybrid.Interfaces.Services;

namespace BlazorHybrid.Services
{
    public class HttpService(HttpClient httpClient) : IHttpService
    {
        private readonly HttpClient _httpClient =
            httpClient ?? throw new ArgumentNullException(nameof(httpClient));

        public Uri BaseAddress => _httpClient.BaseAddress;

        public async Task<T> GetAsync<T>(string uri, string? token = null)
        {
            SetAuthorizationHeader(token);

            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string uri, TRequest content, string? token = null)
        {
            SetAuthorizationHeader(token);

            var response = await _httpClient.PostAsJsonAsync(uri, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TResponse>() ??
                   throw new InvalidOperationException("Empty response from the server.");
        }

        public async Task PostAsync<T>(string uri, T content, string? token = null)
        {
            SetAuthorizationHeader(token);

            var response = await _httpClient.PostAsJsonAsync(uri, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task PutAsync<T>(string uri, T content, string? token = null)
        {
            SetAuthorizationHeader(token);

            var response = await _httpClient.PutAsJsonAsync(uri, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(string uri, string? token = null)
        {
            SetAuthorizationHeader(token);

            var response = await _httpClient.DeleteAsync(uri);
            response.EnsureSuccessStatusCode();
        }

        private void SetAuthorizationHeader(string? token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }
    }
}

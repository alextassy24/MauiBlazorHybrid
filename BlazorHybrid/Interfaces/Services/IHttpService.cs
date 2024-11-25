namespace BlazorHybrid.Interfaces.Services
{
    public interface IHttpService
    {
        Uri BaseAddress { get; }
        Task<T> GetAsync<T>(string uri, string? token = null);
        Task<TResponse> PostAsync<TRequest, TResponse>(string uri, TRequest content, string? token = null);
        Task PostAsync<T>(string uri, T content, string? token = null);
        Task PutAsync<T>(string uri, T content, string? token = null);
        Task DeleteAsync(string uri, string? token = null);
    }
}

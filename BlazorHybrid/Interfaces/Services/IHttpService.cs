
namespace BlazorHybrid.Interfaces.Services
{
    public interface IHttpService
    {
        Task<T> GetAsync<T>(string uri);
        Task PostAsync<T>(string uri, T content);
        Task PutAsync<T>(string uri, T content);
        Task DeleteAsync(string uri);
    }
}
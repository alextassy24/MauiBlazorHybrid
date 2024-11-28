using BlazorHybridBackend.Models;

namespace BlazorHybridBackend.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);
    }
}
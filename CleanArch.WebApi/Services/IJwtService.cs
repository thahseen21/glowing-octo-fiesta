using CleanArch.WebApi.Common.Model;

namespace CleanArch.WebApi.Services
{
    public interface IJwtService
    {
        Task<string> GetTokenAsync(AuthRequest authRequest);
    }
}

using System.Security.Claims;

namespace TaskApi.Services.Interfaces
{
    public interface IJWTService
    {
        string GenerateSecurityToken(string id, string email);
    }
}

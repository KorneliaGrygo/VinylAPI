using System.Security.Claims;

namespace VinylAPI.Services
{
    public interface IUserContextService
    {
        int? GetUserId { get; }
        ClaimsPrincipal User { get; }

    }
}
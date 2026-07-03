using System.Security.Claims;
using JiraMini.Application.Common.Interfaces;

namespace JiraMini.Api.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    public Guid? UserId
    {
        get
        {
            var id = User?
                .FindFirst(ClaimTypes.NameIdentifier)?
                .Value;

            return Guid.TryParse(id, out var guid)
                ? guid
                : null;
        }
    }

    public string? Email =>
        User?
            .FindFirst(ClaimTypes.Email)?
            .Value;

    public string? Role =>
        User?
            .FindFirst(ClaimTypes.Role)?
            .Value;

    public bool IsAuthenticated =>
        User?.Identity?.IsAuthenticated ?? false;
}
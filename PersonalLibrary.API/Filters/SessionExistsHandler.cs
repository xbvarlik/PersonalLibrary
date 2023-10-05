using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Net.Http.Headers;
using PersonalLibrary.API.Services;

namespace PersonalLibrary.API.Filters;

public class SessionExistsHandler : AuthorizationHandler<SessionExistsRequirement>
{
    private readonly HttpContextAccessor _httpContextAccessor;
    private readonly SessionCacheService _sessionCacheService;


    public SessionExistsHandler(HttpContextAccessor httpContextAccessor, SessionCacheService sessionCacheService)
    {
        _httpContextAccessor = httpContextAccessor;
        _sessionCacheService = sessionCacheService;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, SessionExistsRequirement requirement)
    {
        if (context.PendingRequirements.Any(x => x.GetType() == typeof(DenyAnonymousAuthorizationRequirement)))
        {
            context.Fail();
            return;
        }

        var token = _httpContextAccessor.HttpContext?.Request.Headers[HeaderNames.Authorization].ToString()
            .Replace("Bearer", "", StringComparison.OrdinalIgnoreCase).Trim();

        if (string.IsNullOrWhiteSpace(token))
        {
            context.Fail();
            return;
        }

        var session = await _sessionCacheService.GetOrAddAsync(token);
        if (session != default)
            context.Succeed(requirement);
        else
            context.Fail();
    }
}
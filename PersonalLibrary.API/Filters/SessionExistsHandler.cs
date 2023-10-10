using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using PersonalLibrary.API.Services;
using PersonalLibrary.Utilities.Accessors;

namespace PersonalLibrary.API.Filters;

public class SessionExistsHandler : AuthorizationHandler<SessionExistsRequirement>
{
    private readonly ISessionAccessor _sessionAccessor;
    private readonly SessionService _sessionService;
    
    public SessionExistsHandler(SessionService sessionService, ISessionAccessor sessionAccessor)
    {
        _sessionService = sessionService;
        _sessionAccessor = sessionAccessor;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, SessionExistsRequirement requirement)
    {
        if (context.PendingRequirements.Any(x => x.GetType() == typeof(DenyAnonymousAuthorizationRequirement)))
        {
            context.Fail();
            return;
        }

        var token = _sessionAccessor.GetAccessToken();

        if (string.IsNullOrWhiteSpace(token))
        {
            context.Fail();
            return;
        }

        var session = await _sessionAccessor.GetOrAddAsync(_sessionService.GetSessionByAccessTokenAsync);
        if (session != default)
            context.Succeed(requirement);
        else
            context.Fail();
    }
}
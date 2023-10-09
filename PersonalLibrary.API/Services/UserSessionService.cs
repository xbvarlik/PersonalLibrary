using Microsoft.Net.Http.Headers;
using PersonalLibrary.API.DTOs.AuthDTOs;
using PersonalLibrary.Exceptions;
using PersonalLibrary.Repository.MongoDB.MongoDbEntities;

namespace PersonalLibrary.API.Services;

public class UserSessionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly SessionCacheService _sessionCacheService;
    
    private readonly string _accessToken;
    public readonly Session<LoginDetailsDto> Context;
    
    public UserSessionService(
        IHttpContextAccessor httpContextAccessor,
        SessionCacheService sessionCacheService)
    {
        _httpContextAccessor = httpContextAccessor;
        _sessionCacheService = sessionCacheService;
        _accessToken = GetAccessToken();
        Context = GetContext();
    }
    
    private string GetAccessToken()
    {
        var accessToken =
            _httpContextAccessor.HttpContext?.Request.Headers[HeaderNames.Authorization]
                .ToString()
                .Replace("Bearer", "", StringComparison.OrdinalIgnoreCase)
                .Trim();

        return accessToken ?? throw new UnauthorizedException();
    }

    private Session<LoginDetailsDto> GetContext()
    {
        var session = _sessionCacheService.GetOrAddAsync(_accessToken).GetAwaiter().GetResult();
        return session ?? throw new UnauthorizedException();
    }
}
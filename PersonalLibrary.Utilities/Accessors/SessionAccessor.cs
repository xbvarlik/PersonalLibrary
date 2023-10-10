using Microsoft.AspNetCore.Http;
using PersonalLibrary.Cache;
using PersonalLibrary.Core.DTOs.AuthDTOs;
using PersonalLibrary.Core.MongoDocuments;

namespace PersonalLibrary.Utilities.Accessors;

public class SessionAccessor : ISessionAccessor
{
    private readonly ICacheManager _cacheManager;
    private readonly string? _accessToken;

    public SessionAccessor(IHttpContextAccessor httpContextAccessor, ICacheManager cacheManager)
    {
        _cacheManager = cacheManager;
        _accessToken = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString()
            .Replace("Bearer", "", StringComparison.OrdinalIgnoreCase)
            .Trim();
    }

    public int AccessUserId()
    {
        var userSessionInfo = _cacheManager.GetAsync<Session<LoginDetailsDto>>(_accessToken!).Result;
        return userSessionInfo?.UserId ?? 0;
    }

    public async Task<int> AccessUserIdAsync()
    {
        var userSessionInfo = await _cacheManager.GetAsync<Session<LoginDetailsDto>>(_accessToken!);
        return userSessionInfo?.UserId ?? 0;
    }

    public async Task<T?> GetOrAddAsync<T>(Func<string, Task<T?>> func) where T : class?
        => await _cacheManager.GetOrAddAsync(_accessToken!, func);
    
    public string? GetAccessToken() => _accessToken;
}


using PersonalLibrary.API.DTOs.AuthDTOs;
using PersonalLibrary.Cache;
using PersonalLibrary.Repository.MongoDB.MongoDbEntities;

namespace PersonalLibrary.API.Services;

public class SessionCacheService
{
    private readonly ICacheManager _cacheManager;
    private readonly SessionService _sessionService;

    public SessionCacheService(ICacheManager cacheManager, SessionService sessionService)
    {
        _cacheManager = cacheManager;
        _sessionService = sessionService;
    }
    
    public async Task<Session<LoginDetailsDto>?> GetOrAddAsync(string token)
        => await _cacheManager.GetOrAddAsync(token, _ => _sessionService.GetSessionByAccessTokenAsync(token));
}
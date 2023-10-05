using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PersonalLibrary.API.DTOs.AuthDTOs;
using PersonalLibrary.API.Utilities;
using PersonalLibrary.Repository.Entities;
using PersonalLibrary.Repository.MongoDB;
using PersonalLibrary.Repository.MongoDB.MongoDbEntities;

namespace PersonalLibrary.API.Services;

public class SessionService
{
    private readonly IMongoCollection<Session<LoginDetailsDto>> _sessionCollection;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserRoleService _userRoleService;
    private readonly TokenManager _tokenManager;

    public SessionService(IOptions<MongoDbSettings> dbSettings, IHttpContextAccessor httpContextAccessor, TokenManager tokenManager, UserRoleService userRoleService)
    {
        _httpContextAccessor = httpContextAccessor;
        _tokenManager = tokenManager;
        _userRoleService = userRoleService;
        var client = new MongoClient(dbSettings.Value.ConnectionUri);
        var database = client.GetDatabase(dbSettings.Value.DatabaseName);
        _sessionCollection = database.GetCollection<Session<LoginDetailsDto>>(dbSettings.Value.CollectionName);
    }
    
    public async Task<Session<LoginDetailsDto>?> GetSessionAsync(int userId)
    {
        return await _sessionCollection.Find(session => session.UserId == userId).FirstOrDefaultAsync();
    }

    public async Task<Session<LoginDetailsDto>?> GetSessionByAccessTokenAsync(string accessToken)
    {
        return await _sessionCollection.Find(session => session.Login.AccessToken == accessToken).FirstOrDefaultAsync();
    }
    
    
    public async Task<LoginDetailsDto> CreateSessionAsync(User user)
    {
        var userRoles = await _userRoleService.GetUserRolesAsync(user.Id);
        var userRolesString = userRoles.Select(x => x.Name).ToList();
        var token = await _tokenManager.CreateAccessTokenAsync(user);
        
        var login = CreateLoginDetails(user, token);
        var session = CreteSessionEntity(user, userRolesString, login);

        await UpsertUserSessionAsync(session);
        return login;
    }

    public async Task<Session<LoginDetailsDto>> UpsertUserSessionAsync(Session<LoginDetailsDto> session)
    {
        Session<LoginDetailsDto> entity;
        
        var currentSession = await GetSessionAsync(session.UserId);
        if (currentSession is null)
        {
            await _sessionCollection.InsertOneAsync(session);
            entity = session;
        }
        else
        {
            await _sessionCollection.ReplaceOneAsync(x => x.Id == session.Id, session);
            entity = session;
        }

        return entity;
    }

    private Session<LoginDetailsDto> CreteSessionEntity(User user, List<string?> userRolesString, LoginDetailsDto login)
    {
        return new Session<LoginDetailsDto>
        {
            UserId = user.Id,
            Agent = _httpContextAccessor.HttpContext!.Request.Headers["User-Agent"].ToString(),
            Email = user.Email,
            UserRoles = userRolesString,
            Login = login,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    private static LoginDetailsDto CreateLoginDetails(User user, AccessTokenDto token)
    {
        var login = new LoginDetailsDto
        {
            UserId = user.Id,
            AccessToken = token.AccessToken,
            RefreshToken = token.RefreshToken,
            AccessTokenExpiryTime = token.AccessTokenExpirationDateTime,
            RefreshTokenExpiryTime = token.RefreshTokenExpirationDateTime
        };
        return login;
    }
}
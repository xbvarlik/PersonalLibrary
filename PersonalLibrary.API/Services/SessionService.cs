using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PersonalLibrary.API.DTOs.AuthDTOs;
using PersonalLibrary.API.Utilities;
using PersonalLibrary.Exceptions;
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
        var response = await _sessionCollection.Find(session => session.Login.AccessToken == accessToken).FirstOrDefaultAsync();
        return response;
    }
    
    
    public async Task<LoginDetailsDto> CreateSessionAsync(User user)
    {
        var userRoles = await _userRoleService.GetUserRolesAsync(user.Id);
        
        if(userRoles is null) throw new NotFoundException("User does not have roles.");
        
        var token = await _tokenManager.CreateAccessTokenAsync(user);
        
        var login = CreateLoginDetails(user, token);
        var session = CreteSessionEntity(user, userRoles!, login);

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

    private Session<LoginDetailsDto> CreteSessionEntity(User user, IList<string> userRoles, LoginDetailsDto login)
    {
        return new Session<LoginDetailsDto>
        {
            UserId = user.Id,
            Agent = _httpContextAccessor.HttpContext!.Request.Headers["User-Agent"].ToString(),
            Email = user.Email!,
            UserRoles = userRoles,
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
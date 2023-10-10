using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PersonalLibrary.API.Constants;
using PersonalLibrary.Core.DTOs.AuthDTOs;
using PersonalLibrary.Core.Entities;
using PersonalLibrary.Exceptions;
using PersonalLibrary.Repository;

namespace PersonalLibrary.API.Services;

public class TokenService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly JwtBearerTokenSettings _jwtBearerTokenSettings;
    private readonly AppDbContext _appDbContext;
    private readonly DbSet<AppRefreshToken> _dbSet;

    public TokenService(UserManager<User> userManager, RoleManager<Role> roleManager, IOptions<JwtBearerTokenSettings> jwtBearerTokenSettings, AppDbContext appDbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _appDbContext = appDbContext;
        _jwtBearerTokenSettings = jwtBearerTokenSettings.Value;
        _dbSet = _appDbContext.AppRefreshTokens;
    }

    private static SecurityKey GetSymmetricSecurityKey(string securityKey)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
    }
    
    private async Task<IEnumerable<Claim>> GetUserClaimsAsync(User User, string audience)
    {
        var userClaims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, User.Id.ToString()),
            new (JwtRegisteredClaimNames.Email, User.Email!),
            new (ClaimTypes.Name, User.UserName!),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new (JwtRegisteredClaimNames.Aud, audience)
        };

        var userRoles = await _userManager.GetRolesAsync(User);
        
        foreach (var userRole in userRoles)
        {
            userClaims.Add(new Claim(ClaimTypes.Role, userRole));
            
            var roleNames = await _roleManager.FindByNameAsync(userRole);
            if (roleNames == null) continue;
            var roleClaims = await _roleManager.GetClaimsAsync(roleNames);

            userClaims.AddRange(roleClaims.Select(claim => new Claim(claim.Type, claim.Value)));
        }

        return userClaims;
    }
    
    private static string CreateRefreshToken()
    {
        var numberBytes = new byte[32];
        using var random = RandomNumberGenerator.Create();
        random.GetBytes(numberBytes);

        return Convert.ToBase64String(numberBytes);
    }
    
    private async Task<AccessTokenDto> CreateToken(User User)
    {
        var accessTokenExpirationDateTime = DateTime.UtcNow.AddMinutes(60);
        var refreshTokenExpirationDateTime = DateTime.UtcNow.AddMinutes(360);
        var securityKey = GetSymmetricSecurityKey(_jwtBearerTokenSettings.SecurityKey);

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtBearerTokenSettings.Issuer,
            expires: accessTokenExpirationDateTime,
            notBefore: DateTime.UtcNow,
            claims: await GetUserClaimsAsync(User, _jwtBearerTokenSettings.Audiences[0]),
            signingCredentials: signingCredentials
        );
        var jwtHandler = new JwtSecurityTokenHandler();
        var token = jwtHandler.WriteToken(jwtSecurityToken);
        
        return new AccessTokenDto
        {
            AccessToken = token,
            AccessTokenExpirationDateTime = accessTokenExpirationDateTime,
            RefreshToken = CreateRefreshToken(),
            RefreshTokenExpirationDateTime = refreshTokenExpirationDateTime
        };
    }
    
    public async Task<AccessTokenDto> CreateAccessTokenAsync(User User)
    {
        var accessTokenDto = await CreateToken(User);

        var appRefreshToken = await _dbSet.SingleOrDefaultAsync(x => x.UserId == User.Id);

        if (appRefreshToken == null)
        {
            await _dbSet.AddAsync(new AppRefreshToken
            {
                UserId = User.Id,
                RefreshToken = accessTokenDto.RefreshToken,
                RefreshTokenExpirationDateTime = accessTokenDto.RefreshTokenExpirationDateTime
            });
        }
        else
        {
            appRefreshToken.RefreshToken = accessTokenDto.RefreshToken;
            appRefreshToken.RefreshTokenExpirationDateTime =
                accessTokenDto.RefreshTokenExpirationDateTime;
        }

        await _appDbContext.SaveChangesAsync();
        return accessTokenDto;
    }
    
    public async Task<AccessTokenDto> CreateAccessTokenByRefreshTokenAsync(string refreshToken)
    {
        var dbRefreshToken = await _dbSet.SingleOrDefaultAsync(x => x.RefreshToken == refreshToken);

        if (dbRefreshToken == null) throw new TokenNullException("Refresh token not found");

        var user = await _userManager.FindByIdAsync(dbRefreshToken.UserId.ToString());

        if (user == null) throw new NotFoundException("User not found");

        var tokenModel = await CreateToken(user);
        dbRefreshToken.RefreshToken = tokenModel.RefreshToken;
        dbRefreshToken.RefreshTokenExpirationDateTime = tokenModel.RefreshTokenExpirationDateTime;

        await _appDbContext.SaveChangesAsync();
        return tokenModel;
    }
    
    public async Task RevokeRefreshTokenAsync(int UserId)
    {
        var dbRefreshToken = await _dbSet.SingleOrDefaultAsync(rt => rt.UserId == UserId);

        if (dbRefreshToken == null) throw new TokenNullException("Refresh token not found");

        _dbSet.Remove(dbRefreshToken);
        await _appDbContext.SaveChangesAsync();

            
    }
}
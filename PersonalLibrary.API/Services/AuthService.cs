using Microsoft.AspNetCore.Identity;
using PersonalLibrary.API.DTOs.AuthDTOs;
using PersonalLibrary.API.Utilities;
using PersonalLibrary.Exceptions;
using PersonalLibrary.Repository;
using PersonalLibrary.Repository.Entities;

namespace PersonalLibrary.API.Services;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly TokenManager _tokenManager;

    public AuthService(AppDbContext context, UserManager<User> userManager, SignInManager<User> signInManager, TokenManager tokenManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenManager = tokenManager;
    }

    public async Task<IdentityResult> SignUpAsync(SignUpDto signUpDto)
    {
        var user = new User
        {
            UserName = signUpDto.UserName,
            Email = signUpDto.Email,
            FirstName = signUpDto.FirstName,
            LastName = signUpDto.LastName
        };
        
        var password = signUpDto.Password;
        
        var result = await _userManager.CreateAsync(user, password);
        await _context.SaveChangesAsync();

        return result;
    }
    
    public async Task<AccessTokenDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user is null) throw new NotFoundException("User not found");
        
        var password = loginDto.Password;
        var rememberMe = loginDto.RememberMe;
        
        var signInResult = await _signInManager.PasswordSignInAsync(user, password, rememberMe, true);
        
        if (!signInResult.Succeeded) throw new InvalidCredentialsException("Invalid credentials");

        var result = await _tokenManager.CreateAccessTokenAsync(user);
        
        return result;
    }
    
    public async Task LogoutAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user == null) throw new NotFoundException("User not found");
        
        await _tokenManager.RevokeRefreshTokenAsync(user.Id);
        await _signInManager.SignOutAsync();
    }

    public async Task<AccessTokenDto> RefreshAccessTokenAsync(string refreshToken)
    {
        if (refreshToken == null) throw new TokenNullException("Refresh token not found");
        
        return await _tokenManager.CreateAccessTokenByRefreshTokenAsync(refreshToken);
    }
}
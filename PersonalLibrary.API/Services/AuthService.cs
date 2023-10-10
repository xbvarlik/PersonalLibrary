using Microsoft.AspNetCore.Identity;
using PersonalLibrary.Core.DTOs.AuthDTOs;
using PersonalLibrary.Core.Entities;
using PersonalLibrary.Exceptions;
using PersonalLibrary.Repository;
using PersonalLibrary.Utilities.Managers;

namespace PersonalLibrary.API.Services;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly TokenService _tokenService;
    private readonly IConfiguration _configuration;
    private readonly EmailManager _emailManager;

    public AuthService(AppDbContext context, UserManager<User> userManager, SignInManager<User> signInManager, TokenService tokenService, EmailManager emailManager, IConfiguration configuration)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _emailManager = emailManager;
        _configuration = configuration;
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

        var result = await _tokenService.CreateAccessTokenAsync(user);
        
        return result;
    }
    
    public async Task LogoutAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user == null) throw new NotFoundException("User not found");
        
        await _tokenService.RevokeRefreshTokenAsync(user.Id);
        await _signInManager.SignOutAsync();
    }

    public async Task<AccessTokenDto> RefreshAccessTokenAsync(string refreshToken)
    {
        if (refreshToken == null) throw new TokenNullException("Refresh token not found");
        
        return await _tokenService.CreateAccessTokenByRefreshTokenAsync(refreshToken);
    }
    
    public async Task<string> ForgotPasswordAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        
        if (user is null) throw new NotFoundException("User not found");
        
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var appUrl = _configuration["profiles:https:applicationUrl"];
        var callbackUrl = $"{appUrl}/api/auth/reset-password?email={email}&token={token}";

        Console.WriteLine(callbackUrl);
        
        await _emailManager.SendEmail(callbackUrl, email);
        
        return "Reset password url sent to your email";
    }

    public async Task<IdentityResult> ResetPasswordAsync(int userId, string token, ResetPasswordDto dto)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        
        if (user is null) throw new NotFoundException("User not found");
        
        var result = await _userManager.ResetPasswordAsync(user, token, dto.NewPassword);
        
        if (!result.Succeeded) throw new IdentityException("Password can not be reset");
        
        return result;
    }
}
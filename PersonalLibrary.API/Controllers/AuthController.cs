using System.Security.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonalLibrary.API.DTOs.AuthDTOs;
using PersonalLibrary.API.DTOs.CommunicationDTOs;
using PersonalLibrary.API.Services;
using PersonalLibrary.Exceptions;
using PersonalLibrary.Repository.Entities;
using PersonalLibrary.Repository.MongoDB.MongoDbEntities;

namespace PersonalLibrary.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _service;
    private readonly SessionService _sessionService;
    private readonly UserManager<User> _userManager;

    public AuthController(AuthService service, SessionService sessionService, UserManager<User> userManager)
    {
        _service = service;
        _sessionService = sessionService;
        _userManager = userManager;
    }
    
    [HttpPost("signup")]
    public async Task<IActionResult> SignUpAsync(SignUpDto signUpDto)
    {
        var response = await _service.SignUpAsync(signUpDto);
        
        if(response.Succeeded)
            return CreateActionResult(ResponseDto<IdentityResult>.Success(200, response));

        var errorList = new List<string>();
        foreach (var error in response.Errors)
        {
            errorList.Add(error.Description);
        }

        return CreateActionResult(ResponseDto<IdentityResult>.Fail(400, errorList));
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> SignInAsync(LoginDto loginDto)
    {
        var response = await _service.LoginAsync(loginDto);
        
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        await CreateUserSessionAsync(user!);
        
        return CreateActionResult(ResponseDto<AccessTokenDto>.Success(200, response));
    }
    
    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        var userName = HttpContext.User.Identity?.Name;

        if (userName == null) throw new NotFoundException("User not found");
        
        await _service.LogoutAsync(userName);
        return CreateActionResult(ResponseDto<NoContent>.Success(200));
    }
    
    [AllowAnonymous]
    [HttpGet("refresh-token")]
    public async Task<IActionResult> CreateTokenByRefreshTokenAsync(string refreshToken)
    {
        var result = await _service.RefreshAccessTokenAsync(refreshToken);

        return CreateActionResult(ResponseDto<AccessTokenDto>.Success(200, result));
    }
    
    [AllowAnonymous]
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPasswordAsync(string email)
    {
        var result = await _service.ForgotPasswordAsync(email);

        return CreateActionResult(ResponseDto<string>.Success(200, result));
    }
    
    [AllowAnonymous]
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPasswordAsync([FromQuery] int userId, [FromQuery] string token, [FromBody] ResetPasswordDto resetPasswordDto)
    {
        var result = await _service.ResetPasswordAsync(userId, token, resetPasswordDto);

        return CreateActionResult(ResponseDto<IdentityResult>.Success(200 , result));
    }
    
    [NonAction] 
    private IActionResult CreateActionResult<T>(ResponseDto<T> response) 
    { 
        if (response.StatusCode == 204) 
            return new ObjectResult(null) 
            { 
                StatusCode = response.StatusCode 
            };
        return new ObjectResult(response) 
        { 
            StatusCode = response.StatusCode 
        };
    }
    
    private async Task<LoginDetailsDto> CreateUserSessionAsync(User applicationUser)
    {
        if (applicationUser.Email is null)
            throw new InvalidCredentialException("Email cannot be empty");

        var claims = new List<Claim> { new(ClaimTypes.Email, applicationUser.Email) };

        var appIdentity = new ClaimsIdentity(claims);
        HttpContext.User.AddIdentity(appIdentity);

        return await _sessionService.CreateSessionAsync(applicationUser);
    }
}
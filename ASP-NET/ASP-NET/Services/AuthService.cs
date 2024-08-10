using ASP_NET.Dto;
using ASP_NET.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ASP_NET.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;

    private readonly IUserService _userService;
    private readonly UserManager<User> _userManager;

    public AuthService(IConfiguration configuration, UserManager<User> userManager, IUserService userService)
    {
        this._configuration = configuration;
        this._userManager = userManager;
        this._userService = userService;
    }

    public async Task<ResultResponseDto> RegisterAsync(RegisterDto dto)
    {
        var existUser = await this._userManager.FindByNameAsync(dto.Username);

        if (existUser != null)
        {
            return new ResultResponseDto
            {
                Message = "The user already exists with this name",
                Success = false,
            };
        }

        var user = await this._userService.CreateUser(dto);

        return new ResultResponseDto
        {
            Message = "Successfully created",
            Success = true,
        };
    }

    public async Task<ResultResponseDto> LoginAsync(LoginDto dto)
    {

        var user = await this._userManager.FindByEmailAsync(dto.Email);

        if (user == null)
        {
            return new ResultResponseDto
            {
                Message = "User does not exist",
                Success = false
            };
        }

        bool passwordValid = await this._userManager.CheckPasswordAsync(user, dto.Password);

        if (passwordValid == false)
        {
            return new ResultResponseDto
            {
                Message = "Wrong Password",
                Success = false
            };
        }

        var authClaims = new[]
        {
            new Claim("UserId", user.Id),
            new Claim("Username", user.UserName)
        };

        var token = this.GenerateJwtToken(authClaims);

        return new ResultResponseDto
        {
            Message = token,
            Success = true
        };
    }

    public string GenerateJwtToken(Claim[] claims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        var tokenData = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            expires: DateTime.Now.AddHours(3),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        var token = new JwtSecurityTokenHandler().WriteToken(tokenData);

        return token;
    }
}

public interface IAuthService
{
    Task<ResultResponseDto> RegisterAsync(RegisterDto dto);
    Task<ResultResponseDto> LoginAsync(LoginDto dto);
    string GenerateJwtToken(Claim[] claims);
}
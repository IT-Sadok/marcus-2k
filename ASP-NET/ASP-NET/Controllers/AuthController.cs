using ASP_NET.Context;
using ASP_NET.Dto;
using ASP_NET.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ASP_NET.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<User> _passwordHasher;


        public AuthController(ApplicationDbContext context, UserManager<User> userManager, IConfiguration configuration, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }


        [HttpPost("register")]
        public async Task<ActionResult<ResultResponseDto>> Register(RegisterDto request)
        {
            var existUser = await this._userManager.FindByNameAsync(request.Username);

            if (existUser != null)
            {
                return Ok(new ResultResponseDto
                {
                    Message = "The user already exists with this name",
                    Success = false,
                });
            }

            var user = new User { Email = request.Email, UserName = request.Username };

            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            var result = await this._userManager.CreateAsync(user);

            return Ok(new ResultResponseDto
            {
                Message = "Successfully created",
                Success = true,
            });
        }


        [HttpPost("login")]
        public async Task<ActionResult<ResultResponseDto>> Login(LoginDto request)
        {
            var user = await this._userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return Ok(new ResultResponseDto
                {
                    Message = "User does not exist",
                    Success = false
                });
            }

            bool passwordValid = await this._userManager.CheckPasswordAsync(user, request.Password);

            if (passwordValid == false)
            {
                return Ok(new ResultResponseDto
                {
                    Message = "Wrong Password",
                    Success = false
                });
            }

            var tokenId = Guid.NewGuid().ToString();

            var authClaims = new[]
                {
                    new Claim("Username", user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,tokenId)
                };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var tokenData = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenData);

            var tokenEntity = new IdentityUserToken<string>
            {
                UserId = user.Id,
                LoginProvider = "JWT",
                Name = "Token",
                Value = token
            };

            _context.Tokens.Add(tokenEntity);

            await _context.SaveChangesAsync();

            return Ok(new ResultResponseDto
            {
                Message = token,
                Success = true
            });
        }
    }
}

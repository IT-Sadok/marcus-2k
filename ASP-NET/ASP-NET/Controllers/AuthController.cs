using ASP_NET.Dto;
using ASP_NET.Entities;
using ASP_NET.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ASP_NET.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly IUserService _userService;

        private readonly UserManager<User> _userManager;

        public AuthController(UserManager<User> userManager, IAuthService service, IUserService userService)
        {
            this._service = service;
            this._userService = userService;

            this._userManager = userManager;
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

            var user = await this._userService.CreateUser(request);

            return Ok(new ResultResponseDto
            {
                Message = "Successfully created",
                Success = true,
            });
        }


        [HttpPost("login")]
        public async Task<ActionResult<ResultResponseDto>> Login(LoginDto request)
        {
            try
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

                var authClaims = new[]
                {
                  new Claim("UserId", user.Id),
                  new Claim("Username", user.UserName)
                };

                var token = this._service.GenerateJwtToken(authClaims);

                return Ok(new ResultResponseDto
                {
                    Message = token,
                    Success = true
                });
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}

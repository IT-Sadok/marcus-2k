using ASP_NET.Dto;
using ASP_NET.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            this._service = service;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResultResponseDto>> RegisterAsync(RegisterDto requestBody)
        {
            var result = await this._service.RegisterAsync(requestBody);

            return Ok(result);
        }


        [HttpPost("login")]
        public async Task<ActionResult<ResultResponseDto>> Login(LoginDto requestBody)
        {
            var result = await this._service.LoginAsync(requestBody);

            return Ok(result);
        }
    }
}

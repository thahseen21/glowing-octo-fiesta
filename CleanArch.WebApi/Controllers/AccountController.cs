using CleanArch.WebApi.Common.Model;
using CleanArch.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ApiControllerBase
    {
        private readonly IJwtService _jwtService;

        public AccountController(IJwtService jwtService)
        {
            this._jwtService = jwtService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AuthToken([FromBody] AuthRequest authRequest)
        {
            var token = await _jwtService.GetTokenAsync(authRequest);
            if (token == null)
                return Unauthorized();
            return Ok(new AuthResponse { Token = token });
        }
    }
}

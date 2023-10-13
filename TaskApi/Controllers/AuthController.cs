using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskApi.Models;
using TaskApi.Models.DTO;
using TaskApi.Services;
using TaskApi.Services.Interfaces;

namespace TaskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJWTService _jwtService;

        public AuthController(IUserService userService, IJWTService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(RegisterDTO request)
        {
            var existingUser = await _userService.FindUserByEmailAsync(request.Email);
            if (existingUser is not null)
                return Conflict("User already exists");


            var user = await _userService.RegisterAsync(request);
            if(user is not null)
                return GenerateAccesToken(user.Id.ToString(), user.Email);

            return BadRequest(user);
        }

        private string GenerateAccesToken(string id, string email)
        {
            var accessToken = _jwtService.GenerateSecurityToken(id, email);
            return accessToken;
        }
    }
}

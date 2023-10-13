using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskApi.Services.Interfaces;

namespace TaskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IStorageManager _storageManager;

        public ProfileController(IUserService userService, IStorageManager storageManager)
        {
            _userService = userService;
            _storageManager = storageManager;
        }

        [HttpGet("profilePhoto")]
        public async Task<ActionResult<bool>> GetPPAsync(string email)
        {
            var user = await _userService.FindUserByEmailAsync(email);
            if (user is null)
                return false;

            var url = _storageManager.GetSignedUrl(user.ProfilePhoto);
            return Ok(url);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PC.Core.Auth.WebApi.Contract;
using PC.Core.Auth.WebApi.Interfaces;

namespace PC.Core.Auth.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            string token;
            int minutesExpired;
            var user = _userService.IsValid(request.UserName, request.Password, out token, out minutesExpired);
            if (user == null)
            {
                return BadRequest("Invalid credentials");
            }

            return Ok(new
			{
				access_token = token,
				expires_in = minutesExpired
			});
        }
    }
}

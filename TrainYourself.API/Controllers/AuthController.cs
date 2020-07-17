using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrainYourself.API.Dtos;
using TrainYourself.API.Models;
using TrainYourself.API.Services;

namespace TrainYourself.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register (UserForRegisterDto user)
        {
            user.Username = user.Username.ToLower();

            if (await _service.UserExists(user.Username))
                return BadRequest("User already exists");

            var userToCreate = new User
            {
                Username = user.Username,
                Email = user.Email
            };

            var created = await _service.Register(userToCreate, user.Password);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto user)
        {
            user.Username = user.Username.ToLower();
            var token = await _service.AuthenticateUser(user);

            if (string.IsNullOrEmpty(token))
                return Unauthorized();

            return Ok(new {token});
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TrainYourself.API.Dtos;
using TrainYourself.API.Models;
using TrainYourself.API.Repositories;
using TrainYourself.API.Services;

namespace TrainYourself.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IAuthService _service;

        public AuthController(IAuthRepository repo, IAuthService service)
        {
            _repo = repo;
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register (UserForRegisterDto user)
        {
            user.Username = user.Username.ToLower();

            if (await _repo.UserExists(user.Username))
                return BadRequest("User already exists");

            var userToCreate = new User
            {
                Username = user.Username,
                Email = user.Email
            };

            var created = await _repo.Register(userToCreate, user.Password);

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainYourself.API.Dtos;
using TrainYourself.API.Models;
using TrainYourself.API.Repositories;

namespace TrainYourself.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register (UserForRegisterDto user)
        {
            // validate request

            user.Username = user.Username.ToLower();

            if (await _authRepository.UserExists(user.Username))
                return BadRequest("User already exists");

            var userToCreate = new User
            {
                Username = user.Username,
                Email = user.Email
            };

            var created = await _authRepository.Register(userToCreate, user.Password);

            return StatusCode(201);
        }
    }
}
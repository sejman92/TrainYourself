using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TrainYourself.API.Configuration;
using TrainYourself.API.Dtos;
using TrainYourself.API.Repositories;

namespace TrainYourself.API.Services
{
    public class JwtAuthService: IAuthService
    {
        private readonly IAuthRepository _repo;
        private readonly JwtConfiguration _settings;

        public JwtAuthService(IAuthRepository repo, IOptions<JwtConfiguration> settings)
        {
            _repo = repo;
            _settings = settings.Value;
        }
        public async Task<string> AuthenticateUser(UserForLoginDto user)
        {
            var userFromRepo = await _repo.Login(user.Username, user.Password);

            if (userFromRepo == null) return null;

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
                Issuer = _settings.Issuer,
                Audience = _settings.Issuer
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TrainYourself.API.Configuration;
using TrainYourself.API.Dtos;
using TrainYourself.API.Models;
using TrainYourself.API.Repositories;

namespace TrainYourself.API.Services
{
    public class JwtAuthService: IAuthService
    {
        private readonly IMongoRepository<User> _repo;
        private readonly JwtConfiguration _settings;

        public JwtAuthService(IMongoRepository<User> repo, IOptions<JwtConfiguration> settings)
        {
            _repo = repo;
            _settings = settings.Value;
        }
        public async Task<string> AuthenticateUser(UserForLoginDto user)
        {
            var userFromRepo = await Login(user.Username, user.Password);

            if (userFromRepo == null) return null;

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
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


        public async Task<User> Login(string username, string password)
        {
            var user = await _repo.FindOneAsync(x => x.Username == username);

            if (user == null)
                return null;

            return !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt) ? null : user;
        }

        public async Task<User> Register(User user, string password)
        {
            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _repo.InsertOneAsync(user);

            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            var user = await _repo.FindOneAsync(x => x.Username == username);

            return user != null;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != passwordHash[i]) return false;
            }
            return true;
        }
    }
}

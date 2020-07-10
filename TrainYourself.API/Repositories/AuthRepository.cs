using Microsoft.AspNetCore.Connections.Features;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using TrainYourself.API.Configuration;
using TrainYourself.API.Models;

namespace TrainYourself.API.Repositories
{
    public class AuthRepository : IAuthRepository

    {
        private readonly IMongoCollection<User> _users;

        public AuthRepository(IOptions<UsersDatabaseConfiguration> dbSettings)
        {
            var client = new MongoClient(dbSettings.Value.ConnectionString);
            var db = client.GetDatabase(dbSettings.Value.DatabaseName);

            _users = db.GetCollection<User>(dbSettings.Value.CollectionName);
        }

        public async Task<User> Login(string username, string password)
        {
            var user = (await _users.FindAsync<User>(x => x.Username == username)).FirstOrDefault();

            if (user == null)
                return null;

            return !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt) ? null : user;
        }

        public async Task<User> Register(User user, string password)
        {
            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);
            
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _users.InsertOneAsync(user);
            
            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            var user = (await _users.FindAsync<User>(x => x.Username == username)).FirstOrDefault();

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

            for(int i = 0; i<computedHash.Length; i++)
            {
                if (computedHash[i] != passwordHash[i]) return false;
            }
            return true;
        }
    }
}

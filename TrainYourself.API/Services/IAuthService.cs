using System.Threading.Tasks;
using TrainYourself.API.Dtos;
using TrainYourself.API.Models;

namespace TrainYourself.API.Services
{
    public interface IAuthService
    {
        Task<string> AuthenticateUser(UserForLoginDto user);
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}

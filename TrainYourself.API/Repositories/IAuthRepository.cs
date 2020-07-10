using System.Threading.Tasks;
using TrainYourself.API.Models;

namespace TrainYourself.API.Repositories
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);
    }

}

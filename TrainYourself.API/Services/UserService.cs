using System.Collections.Generic;
using System.Threading.Tasks;
using TrainYourself.API.Models;

namespace TrainYourself.API.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string userName, string password);
        IList<User> GetAll();
    }
    public class UserService
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainYourself.API.Dtos;

namespace TrainYourself.API.Services
{
    public interface IAuthService
    {
        Task<string> AuthenticateUser(UserForLoginDto user);
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrainYourself.API.Repositories;
using TrainYourself.API.Services;

namespace TrainYourself.API.Configuration
{
    public static class RegisterServices
    {
        public static void RegisterAllServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAuthService, JwtAuthService>();
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
        }
    }
}

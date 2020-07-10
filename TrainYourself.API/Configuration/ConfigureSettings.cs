using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TrainYourself.API.Configuration
{
    public static class ConfigureSettings
    {
        public static void RegisterSettings(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.Configure<UsersDatabaseConfiguration>(configuration.GetSection(nameof(UsersDatabaseConfiguration)));
            serviceCollection.Configure<JwtConfiguration>(configuration.GetSection(nameof(JwtConfiguration)));
        }
    }
}

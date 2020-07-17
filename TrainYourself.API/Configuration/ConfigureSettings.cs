using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace TrainYourself.API.Configuration
{
    public static class ConfigureSettings
    {
        public static void RegisterSettings(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.Configure<MongoDatabaseConfiguration>(configuration.GetSection(nameof(MongoDatabaseConfiguration)));

            serviceCollection.AddSingleton<IMongoDatabaseConfiguration>(sp =>
                sp.GetRequiredService<IOptions<MongoDatabaseConfiguration>>().Value);

            serviceCollection.Configure<JwtConfiguration>(configuration.GetSection(nameof(JwtConfiguration)));
        }
    }
}

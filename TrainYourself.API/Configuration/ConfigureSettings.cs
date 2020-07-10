using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HighLowGameMaster.Extensions.AspNetCore
{
    public static class GameMasterExtensions
    {
        public static IServiceCollection AddGameMaster(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            // TODO: Make this work.
            //serviceCollection.AddTransient<GameMasterFactory>();
            //serviceCollection.Configure<GameMasterSettings>(configuration.GetSection(nameof(GameMasterSettings.GameMaster)));

            return serviceCollection;
        }
    }
}
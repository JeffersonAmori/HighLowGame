using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HighLowGameMaster.Extensions.AspNetCore
{
    public static class GameMasterExtensions
    {
        /// <summary>
        /// Register the Game Master on the AspNet Core DI.
        /// </summary>
        /// <param name="builder">The Host Builder</param>
        /// <returns>The Host Builder</returns>
        public static IHostBuilder AddGameMaster(this IHostBuilder builder)
        {
            builder.ConfigureServices((c, s) => s.AddTransient(typeof(GameMasterFactory), typeof(GameMasterFactory)));
            builder.ConfigureServices((c, s) => s.Configure<GameMasterSettings>(settings => c.Configuration.GetSection(nameof(GameMasterSettings.GameMaster)).Bind(settings)));

            return builder;
        }
    }
}
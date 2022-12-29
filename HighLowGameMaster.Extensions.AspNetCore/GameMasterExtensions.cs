using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;

namespace HighLowGameMaster.Extensions.AspNetCore
{
    public static class GameMasterExtensions
    {

        public static IHostBuilder AddGameMaster(this IHostBuilder builder)
        {
            builder.ConfigureServices((c, s) => s.AddTransient(typeof(GameMasterFactory), typeof(GameMasterFactory)));
            builder.ConfigureServices((c, s) => s.Configure<GameMasterSettings>(settings => c.Configuration.GetSection(nameof(GameMasterSettings.GameMaster)).Bind(settings)));

            return builder;
        }
    }
}
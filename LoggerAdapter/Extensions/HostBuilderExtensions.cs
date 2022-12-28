using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LoggerAdapter.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseGenericLogAdapter(this IHostBuilder builder)
        {
            builder.ConfigureServices((c, s) => s.AddTransient(typeof(ILoggerAdapter<>), typeof(GenericLoggerAdapter<>)));

            return builder;
        }
    }
}

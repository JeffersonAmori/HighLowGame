using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LoggerAdapter.Extensions
{
    /// <summary>
    /// Helper class to register the <see cref="ILoggerAdapter{T}"/>.
    /// </summary>
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// Configure the <see cref="ILoggerAdapter{T}"/> in the Services.
        /// </summary>
        /// <param name="builder">The app host.</param>
        /// <returns>The <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder UseGenericLogAdapter(this IHostBuilder builder)
        {
            builder.ConfigureServices((c, s) => s.AddTransient(typeof(ILoggerAdapter<>), typeof(GenericLoggerAdapter<>)));

            return builder;
        }
    }
}

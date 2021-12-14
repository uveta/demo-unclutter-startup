using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Demo.Extensions
{
    public static class TelemetryExtensions
    {
        public static IServiceCollection ConfigureTelemetry(this IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();
            services.AddSingleton<IConfigureOptions<ApplicationInsightsServiceOptions>, ConfigureApplicationInsights>();
            return services;
        }
    }

    public class ConfigureApplicationInsights : IConfigureOptions<ApplicationInsightsServiceOptions>
    {
        public void Configure(ApplicationInsightsServiceOptions options)
        {
            options.EnableHeartbeat = true;
            options.EnableAppServicesHeartbeatTelemetryModule = true;
        }
    }
}
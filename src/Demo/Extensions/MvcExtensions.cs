using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;

namespace Demo.Extensions
{
    public static class MvcExtensions
    {
        public static IServiceCollection ConfigureMvc(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddSingleton<IConfigureOptions<MvcOptions>, ConfigureMvcOptions>();
            services.AddSingleton<IConfigureOptions<MvcNewtonsoftJsonOptions>, ConfigureNewtonsoftOptions>();
            return services;
        }
    }

    public class ConfigureMvcOptions : IConfigureOptions<MvcOptions>
    {
        public void Configure(MvcOptions options)
        {
            options.SuppressAsyncSuffixInActionNames = true;
        }
    }

    public class ConfigureNewtonsoftOptions : IConfigureOptions<MvcNewtonsoftJsonOptions>
    {
        public void Configure(MvcNewtonsoftJsonOptions options)
        {
            options.SerializerSettings.Converters.Add(new StringEnumConverter());
        }
    }
}
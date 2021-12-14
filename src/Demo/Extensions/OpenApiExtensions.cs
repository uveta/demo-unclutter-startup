using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Demo.Extensions
{
    public static class OpenApiExtensions
    {
        public static IServiceCollection ConfigureOpenApi(this IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddSingleton<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();
            services.AddSingleton<IConfigureOptions<SwaggerOptions>, ConfigureSwaggerOptions>();
            services.AddSingleton<IConfigureOptions<SwaggerUIOptions>, ConfigureSwaggerUiOptions>();
            return services;
        }
    }

    public class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
    {
        public void Configure(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo", Version = "v1" });
        }
    }

    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerOptions>
    {
        public void Configure(SwaggerOptions options)
        {
            options.RouteTemplate = "swagger/{documentName}/swagger.json";
            options.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
            {
                swaggerDoc.Servers = new[] {
                    new OpenApiServer {
                        Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{httpReq.PathBase}",
                        Description = "Default"
                    }
                };
            });
        }
    }

    public class ConfigureSwaggerUiOptions : IConfigureOptions<SwaggerUIOptions>
    {
        public void Configure(SwaggerUIOptions options)
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo v1");
        }
    }

}
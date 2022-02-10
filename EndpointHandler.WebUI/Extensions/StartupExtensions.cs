using System.Net.Http;
using EndpointHandler.Domain.Interfaces.Services;
using EndpointHandler.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EndpointHandler.WebUI.Extensions
{
    public static class StartupExtensions
    {
        public static void ConfigureFileService(this IServiceCollection services, IConfiguration configuration)
        {
            string filePath = configuration.GetValue<string>("FilePath");

            services.AddScoped<IFileService, FileService>(s => new FileService(filePath));
        }

        public static void ConfigureHttpService(this IServiceCollection services, IConfiguration configuration)
        {
            string endpointUri = configuration.GetValue<string>("EndpointToHandle");

            services.AddScoped<IHttpService, HttpService>(
                s => new HttpService(s.GetRequiredService<IHttpClientFactory>().CreateClient(),
                    endpointUri));
        }

    }
}
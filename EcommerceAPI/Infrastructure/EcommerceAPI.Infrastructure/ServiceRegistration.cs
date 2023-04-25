using EcommerceAPI.Application.Services;
using EcommerceAPI.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IFileService, FileService>();
        }
    }
}

using EcommerceAPI.Application.Abstractions.Services;
using EcommerceAPI.Application.Abstractions.Storage;
using EcommerceAPI.Application.Abstractions.Token;
using EcommerceAPI.Infrastructure.Services;
using EcommerceAPI.Infrastructure.Services.Storage;
using EcommerceAPI.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IStorageService, StorageService>();
            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
            serviceCollection.AddScoped<IMailService, MailService>();
        }

        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : Storage, IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();
        }

        #region Add Storage using Enum

        //public static void AddStorage<T>(this IServiceCollection serviceCollection, StorageType storageType)
        //{
        //    switch (storageType)
        //    {
        //        case StorageType.Local:
        //            serviceCollection.AddScoped<IStorage, LocalStorage>();
        //            break;
        //        case StorageType.Azure:
        //            serviceCollection.AddScoped<IStorage, AzureStorage>();
        //            break;
        //        case StorageType.AWS:

        //            break;
        //        default:
        //            serviceCollection.AddScoped<IStorage, LocalStorage>();
        //            break;
        //    }
        //}   

        #endregion
    }
}

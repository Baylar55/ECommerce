using EcommerceAPI.Application.Repositories;
using EcommerceAPI.Application.Repositories.Customer;
using EcommerceAPI.Application.Repositories.File;
using EcommerceAPI.Application.Repositories.InvoiceFile;
using EcommerceAPI.Application.Repositories.Order;
using EcommerceAPI.Application.Repositories.Product;
using EcommerceAPI.Persistence.Contexts;
using EcommerceAPI.Persistence.Repositories.Customer;
using EcommerceAPI.Persistence.Repositories.File;
using EcommerceAPI.Persistence.Repositories.InvoiceFile;
using EcommerceAPI.Persistence.Repositories.Order;
using EcommerceAPI.Persistence.Repositories.Product;
using EcommerceAPI.Persistence.Repositories.ProductImageFile;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<EcommerceAPIDbContext>(options => options.UseNpgsql(Configuration.ConnectionString));
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();
            services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
            services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();
            services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();
            services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
        }
    }
}

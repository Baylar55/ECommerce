using EcommerceAPI.Application.Abstractions.Services;
using EcommerceAPI.Application.Abstractions.Services.Authentication;
using EcommerceAPI.Application.Repositories;
using EcommerceAPI.Application.Repositories.Basket;
using EcommerceAPI.Application.Repositories.BasketItem;
using EcommerceAPI.Application.Repositories.Customer;
using EcommerceAPI.Application.Repositories.File;
using EcommerceAPI.Application.Repositories.InvoiceFile;
using EcommerceAPI.Application.Repositories.Order;
using EcommerceAPI.Application.Repositories.Product;
using EcommerceAPI.Domain.Entities.Identity;
using EcommerceAPI.Infrastructure.Services;
using EcommerceAPI.Persistence.Contexts;
using EcommerceAPI.Persistence.Repositories.Basket;
using EcommerceAPI.Persistence.Repositories.BasketItem;
using EcommerceAPI.Persistence.Repositories.Customer;
using EcommerceAPI.Persistence.Repositories.File;
using EcommerceAPI.Persistence.Repositories.InvoiceFile;
using EcommerceAPI.Persistence.Repositories.Order;
using EcommerceAPI.Persistence.Repositories.Product;
using EcommerceAPI.Persistence.Repositories.ProductImageFile;
using EcommerceAPI.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<EcommerceAPIDbContext>(options => options.UseNpgsql(Configuration.ConnectionString));
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<EcommerceAPIDbContext>();
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
            services.AddScoped<IBasketReadRepository, BasketReadRepository>();
            services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();
            services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
            services.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IExternalAuthentication, AuthService>();
            services.AddScoped<IInternalAuthentication, AuthService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}

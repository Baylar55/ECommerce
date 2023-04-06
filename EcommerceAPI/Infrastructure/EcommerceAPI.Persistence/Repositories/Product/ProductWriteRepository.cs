using EcommerceAPI.Application.Repositories.Product;
using EcommerceAPI.Persistence.Contexts;

namespace EcommerceAPI.Persistence.Repositories.Product
{
    public class ProductWriteRepository : WriteRepository<Domain.Entities.Product>, IProductWriteRepository
    {
        public ProductWriteRepository(EcommerceAPIDbContext context) : base(context) { }
    }
}

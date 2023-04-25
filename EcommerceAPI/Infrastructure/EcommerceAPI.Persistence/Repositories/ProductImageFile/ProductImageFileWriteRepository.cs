using EcommerceAPI.Application.Repositories;
using EcommerceAPI.Persistence.Contexts;

namespace EcommerceAPI.Persistence.Repositories.ProductImageFile
{
    public class ProductImageFileWriteRepository : WriteRepository<Domain.Entities.ProductImageFile>, IProductImageFileWriteRepository
    {
        public ProductImageFileWriteRepository(EcommerceAPIDbContext context) : base(context) { }
    }
}

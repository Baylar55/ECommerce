using EcommerceAPI.Application.Repositories.Order;
using EcommerceAPI.Persistence.Contexts;

namespace EcommerceAPI.Persistence.Repositories.Order
{
    public class OrderWriteRepository : WriteRepository<Domain.Entities.Order>, IOrderWriteRepository
    {
        public OrderWriteRepository(EcommerceAPIDbContext context) : base(context) { }
    }
}

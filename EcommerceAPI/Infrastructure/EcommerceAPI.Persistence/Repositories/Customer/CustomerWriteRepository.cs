using EcommerceAPI.Application.Repositories.Customer;
using EcommerceAPI.Persistence.Contexts;

namespace EcommerceAPI.Persistence.Repositories.Customer
{
    public class CustomerWriteRepository : WriteRepository<Domain.Entities.Customer>, ICustomerWriteRepository
    {
        public CustomerWriteRepository(EcommerceAPIDbContext context) : base(context) { }
    }
}

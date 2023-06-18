using EcommerceAPI.Application.Repositories.CompletedOrder;
using EcommerceAPI.Infrastructure.Repositories;
using EcommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Persistence.Repositories.CompletedOrder
{
    public class CompletedOrderReadRepository : ReadRepository<Domain.Entities.CompletedOrder>, ICompletedOrderReadRepository
    {
        public CompletedOrderReadRepository(EcommerceAPIDbContext context) : base(context)
        {
        }
    }
}

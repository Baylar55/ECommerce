using EcommerceAPI.Application.Repositories.CompletedOrder;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Persistence.Repositories.CompletedOrder
{
    public class CompletedOrderWriteRepository : WriteRepository<Domain.Entities.CompletedOrder>, ICompletedOrderWriteRepository

    {
        public CompletedOrderWriteRepository(EcommerceAPIDbContext context) : base(context)
        {
        }
    }
}

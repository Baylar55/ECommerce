using EcommerceAPI.Application.Repositories.Basket;
using EcommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Persistence.Repositories.Basket
{
    public class BasketWriteRepository : WriteRepository<Domain.Entities.Basket>, IBasketWriteRepository
    {
        public BasketWriteRepository(EcommerceAPIDbContext context) : base(context)
        {
        }
    }
}

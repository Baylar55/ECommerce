using EcommerceAPI.Application.Repositories.BasketItem;
using EcommerceAPI.Infrastructure.Repositories;
using EcommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Persistence.Repositories.BasketItem
{
    public class BasketItemReadRepository : ReadRepository<Domain.Entities.BasketItem>, IBasketItemReadRepository
    {
        public BasketItemReadRepository(EcommerceAPIDbContext context) : base(context)
        {
        }
    }
}

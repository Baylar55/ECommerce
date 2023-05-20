using EcommerceAPI.Application.Repositories.BasketItem;
using EcommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Persistence.Repositories.BasketItem
{
    public class BasketItemWriteRepository : WriteRepository<Domain.Entities.BasketItem>, IBasketItemWriteRepository
    {
        public BasketItemWriteRepository(EcommerceAPIDbContext context) : base(context)
        {
        }
    }
}

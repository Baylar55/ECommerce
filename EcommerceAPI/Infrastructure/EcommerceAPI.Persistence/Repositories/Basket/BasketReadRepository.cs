﻿using EcommerceAPI.Application.Repositories.Basket;
using EcommerceAPI.Infrastructure.Repositories;
using EcommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Persistence.Repositories.Basket
{
    public class BasketReadRepository : ReadRepository<Domain.Entities.Basket>, IBasketReadRepository
    {
        public BasketReadRepository(EcommerceAPIDbContext context) : base(context)
        {
        }
    }
}
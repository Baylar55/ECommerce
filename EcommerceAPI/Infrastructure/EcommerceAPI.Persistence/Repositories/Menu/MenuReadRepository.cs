using EcommerceAPI.Application.Repositories.Menu;
using EcommerceAPI.Infrastructure.Repositories;
using EcommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Persistence.Repositories.Menu
{
    public class MenuReadRepository : ReadRepository<Domain.Entities.Menu>, IMenuReadRepository
    {
        public MenuReadRepository(EcommerceAPIDbContext context) : base(context)
        {
        }
    }
}

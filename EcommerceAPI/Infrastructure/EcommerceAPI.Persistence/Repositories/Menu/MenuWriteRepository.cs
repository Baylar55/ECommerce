using EcommerceAPI.Application.Repositories.Menu;
using EcommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Persistence.Repositories.Menu
{
    public class MenuWriteRepository : WriteRepository<Domain.Entities.Menu>, IMenuWriteRepository
    {
        public MenuWriteRepository(EcommerceAPIDbContext context) : base(context)
        {
        }
    }
}

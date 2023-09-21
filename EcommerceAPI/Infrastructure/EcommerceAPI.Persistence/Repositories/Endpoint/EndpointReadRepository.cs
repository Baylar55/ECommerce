using EcommerceAPI.Application.Repositories.Endpoint;
using EcommerceAPI.Infrastructure.Repositories;
using EcommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Persistence.Repositories.Endpoint
{
    public class EndpointReadRepository : ReadRepository<Domain.Entities.Endpoint>, IEndpointReadRepository
    {
        public EndpointReadRepository(EcommerceAPIDbContext context) : base(context)
        {
        }
    }
}

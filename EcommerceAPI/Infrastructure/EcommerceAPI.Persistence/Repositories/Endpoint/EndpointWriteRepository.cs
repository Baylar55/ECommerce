using EcommerceAPI.Application.Repositories.Endpoint;
using EcommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Persistence.Repositories.Endpoint
{
    public class EndpointWriteRepository : WriteRepository<Domain.Entities.Endpoint>, IEndpointWriteRepository
    {
        public EndpointWriteRepository(EcommerceAPIDbContext context) : base(context)
        {
        }
    }
}

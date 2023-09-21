using EcommerceAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Features.Queries.EndpointAuthorization.GetRolesToEndpoint
{
    public class GetRolesToEndpointQueryHandler : IRequestHandler<GetRolesToEndpointQueryRequest, GetRolesToEndpointQueryResponse>
    {
        readonly IEndpointAuthorizationService _endpointAuthorizationService;
        public GetRolesToEndpointQueryHandler(IEndpointAuthorizationService endpointAuthorizationService)
        {
            _endpointAuthorizationService = endpointAuthorizationService;
        }

        public async Task<GetRolesToEndpointQueryResponse> Handle(GetRolesToEndpointQueryRequest request, CancellationToken cancellationToken)
        {
            var datas = await _endpointAuthorizationService.GetRolesToEndpointAsync(request.Code, request.Menu);
            return new(){ Roles = datas };

        }
    }
}

using EcommerceAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Features.Commands.EndpointAuthorization.AssignRoleEndpoint
{
    public class AssignRoleEndpointCommandHandler : IRequestHandler<AssignRoleEndpointCommandRequest, AssignRoleEndpointCommandResponse>
    {
        readonly IEndpointAuthorizationService _endpointAuthorizationService;
        public AssignRoleEndpointCommandHandler(IEndpointAuthorizationService endpointAuthorizationService)
        {
            _endpointAuthorizationService = endpointAuthorizationService;
        }

        public async Task<AssignRoleEndpointCommandResponse> Handle(AssignRoleEndpointCommandRequest request, CancellationToken cancellationToken)
        {
            await _endpointAuthorizationService.AssignRoleEndpointAsync(request.Roles, request.Menu, request.Code, request.Type);

            return new();
        }
    }
}

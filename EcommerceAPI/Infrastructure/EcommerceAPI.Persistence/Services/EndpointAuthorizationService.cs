using EcommerceAPI.Application.Abstractions.Services;
using EcommerceAPI.Application.Abstractions.Services.Configurations;
using EcommerceAPI.Application.Repositories.Endpoint;
using EcommerceAPI.Application.Repositories.Menu;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Persistence.Services
{
    public class EndpointAuthorizationService : IEndpointAuthorizationService
    {
        readonly IApplicationService _applicationService;
        readonly IEndpointReadRepository _endpointReadRepository;
        readonly IEndpointWriteRepository _endpointwriteRepository;
        readonly IMenuReadRepository _menuReadRepository;
        readonly IMenuWriteRepository _menuwriteRepository;
        readonly RoleManager<AppRole> _roleManager;

        public EndpointAuthorizationService(IApplicationService applicationService, IEndpointReadRepository endpointReadRepository, IEndpointWriteRepository endpointwriteRepository, IMenuReadRepository menuReadRepository, IMenuWriteRepository menuwriteRepository)
        {
            _applicationService = applicationService;
            _endpointReadRepository = endpointReadRepository;
            _endpointwriteRepository = endpointwriteRepository;
            _menuReadRepository = menuReadRepository;
            _menuwriteRepository = menuwriteRepository;
        }

        public async Task AssignRoleEndpointAsync(string[] roles, string menu, string code, Type type)
        {
            Menu _menu = await _menuReadRepository.GetSingleAsync(m => m.Name == menu);
            if (_menu == null)
            {
                _menu = new()
                {
                    Id = Guid.NewGuid(),
                    Name = menu,
                };

                await _menuwriteRepository.AddAsync(_menu);
            }
            Endpoint? endpoint = await _endpointReadRepository.Table.Include(e => e.Menu).Include(e => e.AppRoles).FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);
            if (endpoint == null)
            {
                var action = _applicationService.GetAuthorizeDefinitionEndpoint(type).FirstOrDefault(m => m.Name == menu)?.Actions.FirstOrDefault(e => e.Code == code);

                endpoint = new()
                {
                    Code = action.Code,
                    ActionType = action.ActionType,
                    HttpType = action.HttpType,
                    Definition = action.Definition,
                    Id = Guid.NewGuid(),
                    Menu = _menu,
                };

                await _endpointwriteRepository.AddAsync(endpoint);
                await _endpointwriteRepository.SaveAsync();
            }

            foreach (var role in endpoint.AppRoles)
                endpoint.AppRoles.Remove(role);

            var appRoles = await _roleManager.Roles.Where(r => roles.Contains(r.Name)).ToListAsync();

            foreach (var role in appRoles)
                endpoint.AppRoles.Add(role);

            await _endpointwriteRepository.SaveAsync();

        }

        public async Task<List<string>> GetRolesToEndpointAsync(string code, string menu)
        {
            Endpoint? endpoint = await _endpointReadRepository.Table.Include(e => e.AppRoles).Include(e => e.Menu).FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);
            if (endpoint != null)
                return endpoint.AppRoles.Select(r => r.Name).ToList();
            return null;
        }
    }
}

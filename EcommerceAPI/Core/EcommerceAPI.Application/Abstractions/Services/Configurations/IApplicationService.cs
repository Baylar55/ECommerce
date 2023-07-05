using EcommerceAPI.Application.DTOs.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Abstractions.Services.Configurations
{
    public interface IApplicationService
    {
        List<MenuDTO> GetAuthorizeDefinitionEndpoint(Type type);
    }
}

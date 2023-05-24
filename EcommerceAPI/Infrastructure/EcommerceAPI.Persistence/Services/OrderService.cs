using EcommerceAPI.Application.Abstractions.Services;
using EcommerceAPI.Application.DTOs.Order;
using EcommerceAPI.Application.Repositories.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Persistence.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderWriteRepository _orderWriteRepository;

        public OrderService(IOrderWriteRepository orderWriteRepository)
        {
            _orderWriteRepository = orderWriteRepository;
        }

        public async Task CreateOrderAsync(CreateOrderDTO model)
        {
            await _orderWriteRepository.AddAsync(new()
            {
                Address = model.Address,
                Id = Guid.Parse(model.BasketId),
                Description = model.Description,                
            });

            await _orderWriteRepository.SaveAsync();
        }
    }
}

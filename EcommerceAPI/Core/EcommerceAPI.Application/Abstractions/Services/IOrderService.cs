using EcommerceAPI.Application.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Abstractions.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrderDTO model);
        Task<GetAllOrdersDTO> GetAllOrdersAsync(int page, int size);
        Task<GetSingleOrderDTO> GetOrderByIdAsync(string id);
        Task CompleteOrderAsync(string id);
    }
}

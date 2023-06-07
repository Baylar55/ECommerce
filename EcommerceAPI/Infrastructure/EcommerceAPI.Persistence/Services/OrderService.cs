using EcommerceAPI.Application.Abstractions.Services;
using EcommerceAPI.Application.DTOs.Order;
using EcommerceAPI.Application.Repositories.Order;
using Microsoft.EntityFrameworkCore;
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
        private readonly IOrderReadRepository _orderReadRepository;

        public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
        }

        public async Task CreateOrderAsync(CreateOrderDTO model)
        {
            var orderCode = (new Random().NextDouble() * 10000).ToString();
            orderCode.Substring(orderCode.IndexOf(".") + 1, orderCode.Length - orderCode.IndexOf(".") - 1);
            await _orderWriteRepository.AddAsync(new()
            {
                Address = model.Address,
                Id = Guid.Parse(model.BasketId),
                Description = model.Description,
                OrderCode = orderCode
            });

            await _orderWriteRepository.SaveAsync();
        }

        public async Task<GetAllOrdersDTO> GetAllOrdersAsync(int page, int size)
        {
            var query = _orderReadRepository.Table.Include(o => o.Basket)
                                        .ThenInclude(b => b.User)
                                      .Include(o => o.Basket)
                                        .ThenInclude(b => b.BasketItems)
                                        .ThenInclude(bi => bi.Product);

            var data = query.Skip(page * size).Take(size);
            return new()
            {
                TotalOrderCount = await query.CountAsync(),
                Orders = await data.Select(o => new
                {
                    Id = o.Id,
                    CreatedDate = o.CreatedDate,
                    OrderCode = o.OrderCode,
                    TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                    Username = o.Basket.User.UserName
                }).ToListAsync()
            };
        }

        public async Task<GetSingleOrderDTO> GetOrderByIdAsync(string id)
        {
            var data = await _orderReadRepository.Table
                                                 .Include(o => o.Basket)
                                                    .ThenInclude(b => b.BasketItems)
                                                        .ThenInclude(bi => bi.Product)
                                                        .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));
            return new()
            {
                Id=data.Id.ToString(),
                BasketItems= data.Basket.BasketItems.Select(bi=>new
                {
                    bi.Product.Name,
                    bi.Product.Price,
                    bi.Quantity
                }),
                Address = data.Address,
                CreatedDate= data.CreatedDate,
                Description= data.Description,
                OrderCode = data.OrderCode
            };
        }
    }
}

using EcommerceAPI.Application.Abstractions.Services;
using EcommerceAPI.Application.DTOs.Order;
using EcommerceAPI.Application.Repositories.CompletedOrder;
using EcommerceAPI.Application.Repositories.Order;
using EcommerceAPI.Domain.Entities;
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
        private readonly ICompletedOrderWriteRepository _completedOrderWriteRepository;
        private readonly ICompletedOrderReadRepository _completedOrderReadRepository;

        public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository, ICompletedOrderWriteRepository completedOrderWriteRepository, ICompletedOrderReadRepository completedOrderReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
            _completedOrderWriteRepository = completedOrderWriteRepository;
            _completedOrderReadRepository = completedOrderReadRepository;
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

            var data2 = from order in data
                        join completedOrder in _completedOrderReadRepository.Table
                        on order.Id equals completedOrder.OrderId into co
                        from _co in co.DefaultIfEmpty()
                        select new
                        {
                            Id = order.Id,
                            CreatedDate = order.CreatedDate,
                            OrderCode = order.OrderCode,
                            Basket = order.Basket,
                            Completed = _co != null ? true : false
                        };

            return new()
            {
                TotalOrderCount = await query.CountAsync(),
                Orders = await data2.Select(o => new
                {
                    Id = o.Id,
                    CreatedDate = o.CreatedDate,
                    OrderCode = o.OrderCode,
                    TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                    Username = o.Basket.User.UserName,
                    o.Completed
                }).ToListAsync()
            };
        }

        public async Task<GetSingleOrderDTO> GetOrderByIdAsync(string id)
        {
            var data = _orderReadRepository.Table
                                                 .Include(o => o.Basket)
                                                    .ThenInclude(b => b.BasketItems)
                                                        .ThenInclude(bi => bi.Product);
            var data2 = await (from order in data
                        join completedOrder in _completedOrderReadRepository.Table
                        on order.Id equals completedOrder.OrderId into co
                        from _co in co.DefaultIfEmpty()
                        select new
                        {
                            Id = order.Id,
                            CreatedDate = order.CreatedDate,
                            OrderCode = order.OrderCode,
                            Basket = order.Basket,
                            Completed = _co != null ? true : false,
                            Address = order.Address,
                            Description = order.Description,
                        }).FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));
            return new()
            {
                Id = data2.Id.ToString(),
                BasketItems = data2.Basket.BasketItems.Select(bi => new
                {
                    bi.Product.Name,
                    bi.Product.Price,
                    bi.Quantity
                }),
                Address = data2.Address,
                CreatedDate = data2.CreatedDate,
                Description = data2.Description,
                OrderCode = data2.OrderCode,
                Completed = data2.Completed
            };
        }

        public async Task CompleteOrderAsync(string id)
        {
            Order order = await _orderReadRepository.GetByIdAsync(id);
            if (order != null)
            {
                await _completedOrderWriteRepository.AddAsync(new() { OrderId = Guid.Parse(id) });
                await _completedOrderWriteRepository.SaveAsync();
            }
        }
    }
}

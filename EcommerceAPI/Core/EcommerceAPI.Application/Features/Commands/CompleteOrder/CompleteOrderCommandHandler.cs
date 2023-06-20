using EcommerceAPI.Application.Abstractions.Services;
using EcommerceAPI.Application.DTOs.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Features.Commands.CompleteOrder
{
    public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommandRequest, CompleteOrderCommandResponse>
    {
        private readonly IOrderService _orderService;
        private readonly IMailService _mailService;

        public CompleteOrderCommandHandler(IOrderService orderService, IMailService mailService)
        {
            _orderService = orderService;
            _mailService = mailService;
        }

        public async Task<CompleteOrderCommandResponse> Handle(CompleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
            (bool isSucceeded, CompletedOrderDTO model) = await _orderService.CompleteOrderAsync(request.Id);
            if (isSucceeded)
                await _mailService.SendCompletedOrderMailAsync(model.EMail, model.Username, model.OrderCode, model.OrderDate);
            return new();
        }
    }
}

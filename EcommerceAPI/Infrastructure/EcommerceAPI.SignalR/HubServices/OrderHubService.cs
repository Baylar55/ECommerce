using EcommerceAPI.Application.Abstractions.Hubs;
using EcommerceAPI.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.SignalR.HubServices
{
    public class OrderHubService : IOrderHubService
    {
        private readonly IHubContext<OrderHub> _hubContext;

        public OrderHubService(IHubContext<OrderHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task OrderAddedMessageAsync(string message) 
            => _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.OrderAddedMessage,message);
        
    }
}

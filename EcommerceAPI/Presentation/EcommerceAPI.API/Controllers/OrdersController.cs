using EcommerceAPI.Application.Constants;
using EcommerceAPI.Application.CustomAttributes;
using EcommerceAPI.Application.Enums;
using EcommerceAPI.Application.Features.Commands.CompleteOrder;
using EcommerceAPI.Application.Features.Commands.Order.CreateOrder;
using EcommerceAPI.Application.Features.Queries.Order.GetAllOrders;
using EcommerceAPI.Application.Features.Queries.Order.GetOrderById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace EcommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstant.Orders, ActionType = ActionType.Reading, Definition = "Get order by id")]
        public async Task<IActionResult> GetOrderById([FromRoute] GetOrderByIdQueryRequest getOrderByIdQueryRequest)
        {
            GetOrderByIdQueryResponse response = await _mediator.Send(getOrderByIdQueryRequest);
            return Ok(response);
        }

        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstant.Orders, ActionType = ActionType.Reading, Definition = "Get all orders")]
        public async Task<IActionResult> GetAllOrders([FromQuery] GetAllOrdersQueryRequest getAllOrdersQueryRequest)
        {
            GetAllOrdersQueryResponse response = await _mediator.Send(getAllOrdersQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstant.Orders, ActionType = ActionType.Writing, Definition = "Create order")]
        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest createOrderCommandRequest)
        {
            CreateOrderCommandResponse response = await _mediator.Send(createOrderCommandRequest);
            return Ok(response);
        }

        [HttpGet("complete-order/{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstant.Orders, ActionType = ActionType.Updating, Definition = "Completing order")]
        public async Task<IActionResult> CompleteOrder([FromRoute] CompleteOrderCommandRequest completeOrderCommandRequest)
        {
            CompleteOrderCommandResponse response = await _mediator.Send(completeOrderCommandRequest);
            return Ok(response);
        }
    }
}

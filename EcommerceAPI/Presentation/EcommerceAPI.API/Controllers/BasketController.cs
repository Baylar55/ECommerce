using EcommerceAPI.Application.Constants;
using EcommerceAPI.Application.CustomAttributes;
using EcommerceAPI.Application.Enums;
using EcommerceAPI.Application.Features.Commands.Basket.AddItemToBasket;
using EcommerceAPI.Application.Features.Commands.Basket.RemoveBasketItem;
using EcommerceAPI.Application.Features.Commands.Basket.UpdateQuantity;
using EcommerceAPI.Application.Features.Queries.Basket.GetBasketItems;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstant.Basket, ActionType = ActionType.Reading, Definition = "Get basket items")]
        public async Task<IActionResult> GetBasketItems([FromQuery] GetBasketItemsQueryRequest getBasketItemsQueryRequest)
        {
            List<GetBasketItemsQueryResponse> response = await _mediator.Send(getBasketItemsQueryRequest);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstant.Basket, ActionType = ActionType.Writing, Definition = "Add item to basket")]
        public async Task<IActionResult> AddItemToBasket(AddItemToBasketCommandRequest addItemToBasketCommandRequest)
        {
            AddItemToBasketCommandResponse response = await _mediator.Send(addItemToBasketCommandRequest);
            return Ok(response);
        }

        [HttpPut]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstant.Basket, ActionType = ActionType.Updating, Definition = "Update quantity")]
        public async Task<IActionResult> UpdateQuantity(UpdateQuantityCommandRequest updateQuantityCommandRequest)
        {
            UpdateQuantityCommandResponse response = await _mediator.Send(updateQuantityCommandRequest);
            return Ok(response);
        }

        [HttpDelete("{BasketItemId}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstant.Basket, ActionType = ActionType.Deleting, Definition = "Remove basket item")]
        public async Task<IActionResult> RemoveBasketItem([FromRoute] RemoveBasketItemCommandRequest removeBasketItemCommandRequest)
        {
            RemoveBasketItemCommandResponse response = await _mediator.Send(removeBasketItemCommandRequest);
            return Ok(response);
        }
    }
}

using EcommerceAPI.Application.Abstractions.Services;
using EcommerceAPI.Application.Repositories.Basket;
using EcommerceAPI.Application.Repositories.BasketItem;
using EcommerceAPI.Application.Repositories.Order;
using EcommerceAPI.Application.ViewModels.Basket;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Infrastructure.Services
{
    public class BasketService : IBasketService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IBasketWriteRepository _basketWriteRepository;
        private readonly IBasketReadRepository _basketReadRepository;
        private readonly IBasketItemReadRepository _basketItemReadRepository;
        private readonly IBasketItemWriteRepository _basketItemWriteRepository;

        public BasketService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IOrderReadRepository orderReadRepository, IBasketWriteRepository basketWriteRepository, IBasketReadRepository basketReadRepository
            , IBasketItemWriteRepository basketItemWriteRepository, IBasketItemReadRepository basketItemReadRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _orderReadRepository = orderReadRepository;
            _basketWriteRepository = basketWriteRepository;
            _basketReadRepository = basketReadRepository;
            _basketItemReadRepository = basketItemReadRepository;
            _basketItemWriteRepository = basketItemWriteRepository;
        }

        private async Task<Basket?> ContextUser()
        {
            var username = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
            if (!string.IsNullOrEmpty(username))
            {
                AppUser? user = await _userManager.Users.Include(user => user.Baskets).FirstOrDefaultAsync(user => user.UserName == username);

                var _basket = from basket in user.Baskets
                              join order in _orderReadRepository.Table
                              on basket.Id equals order.Id into BasketOrder
                              from order in BasketOrder.DefaultIfEmpty()
                              select new { Basket = basket, Order = order };

                Basket? targetBasket = null;
                if (_basket.Any(b => b.Order is null))
                    targetBasket = _basket.FirstOrDefault(b => b.Order is null)?.Basket;
                else
                {
                    targetBasket = new();
                    user.Baskets.Add(targetBasket);
                }

                await _basketWriteRepository.SaveAsync();
                return targetBasket;
            }
            throw new Exception("An unexpected error has occurred....");
        }

        public async Task<List<BasketItem>> GetBasketItemsAsync()
        {
            Basket? basket = await ContextUser();

            Basket? result = await _basketReadRepository.Table.Include(b => b.BasketItems).ThenInclude(bi => bi.Product).FirstOrDefaultAsync(b => b.Id == basket.Id);

            return result.BasketItems.ToList();
        }

        public async Task AddItemToBasketAsync(BasketItemCreateVM model)
        {
            Basket? basket = await ContextUser();
            if (basket != null)
            {
                BasketItem basketItem = await _basketItemReadRepository.GetSingleAsync(bi => bi.BasketId == basket.Id && bi.ProductId == Guid.Parse(model.ProductId));
                if (basketItem != null)
                    basketItem.Quantity++;
                else
                    await _basketItemWriteRepository.AddAsync(new()
                    {
                        BasketId = basket.Id,
                        ProductId = Guid.Parse(model.ProductId),
                        Quantity = model.Quantity,
                    });
                await _basketWriteRepository.SaveAsync();
            }
        }

        public async Task UpdateQuantityAsync(BasketItemUpdateVM model)
        {
            BasketItem? basketItem = await _basketItemReadRepository.GetByIdAsync(model.BasketItemId);
            if (basketItem != null)
            {
                basketItem.Quantity = model.Quantity;
                await _basketItemWriteRepository.SaveAsync();
            }
        }

        public async Task RemoveBasketItemAsync(string basketItemId)
        {
            BasketItem basketItem = await _basketItemReadRepository.GetByIdAsync(basketItemId);
            if (basketItem != null)
            {
                _basketItemWriteRepository.Remove(basketItem);
                await _basketItemWriteRepository.SaveAsync();
            }
        }

        public Basket GetUserActiveBasket
        {
            get
            {
                Basket? basket = ContextUser().Result;
                return basket;
            }
        }
    }
}

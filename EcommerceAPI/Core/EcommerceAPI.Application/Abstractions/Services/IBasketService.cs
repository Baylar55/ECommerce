using EcommerceAPI.Application.ViewModels.Basket;
using EcommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Abstractions.Services
{
    public interface IBasketService
    {
        public Task<List<BasketItem>> GetBasketItemsAsync();
        public Task AddItemToBasketAsync(BasketItemCreateVM model);
        public Task UpdateQuantityAsync(BasketItemUpdateVM model);
        public Task RemoveBasketItemAsync(string basketItemId);
        public Basket? GetUserActiveBasket { get; }
    }
}

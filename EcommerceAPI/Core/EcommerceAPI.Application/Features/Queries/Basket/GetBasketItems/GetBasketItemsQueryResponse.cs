using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Features.Queries.Basket.GetBasketItems
{
    public class GetBasketItemsQueryResponse
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public string BasketItemId { get; set; }
    }
}

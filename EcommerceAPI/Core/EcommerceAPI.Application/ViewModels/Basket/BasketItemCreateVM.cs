using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.ViewModels.Basket
{
    public class BasketItemCreateVM
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.ViewModels.Basket
{
    public class BasketItemUpdateVM
    {
        public string BasketItemId { get; set; }
        public int Quantity { get; set; }
    }
}

using EcommerceAPI.Domain.Entities.Base;
using EcommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Domain.Entities
{
    public class Basket : BaseEntity
    {
        public AppUser User { get; set; }
        public string UserId { get; set; }
        public Order Order { get; set; }        
        public ICollection<BasketItem> BasketItems { get; set; }
    }
}

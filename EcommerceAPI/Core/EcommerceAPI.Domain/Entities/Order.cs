using EcommerceAPI.Domain.Entities.Base;

namespace EcommerceAPI.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string Description { get; set; }
        public string Address { get; set; }
        public Basket Basket { get; set; }

        //public Guid BasketId { get; set; }        
        //public ICollection<Product> Products { get; set; }
        //public Customer Customer { get; set; }
        //public Guid Customerİd { get; set; }
    }
}

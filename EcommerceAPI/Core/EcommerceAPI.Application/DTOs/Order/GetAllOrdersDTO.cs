using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.DTOs.Order
{
    public class GetAllOrdersDTO
    {
        public int TotalOrderCount { get; set; }
        public object Orders { get; set; }
    }
}

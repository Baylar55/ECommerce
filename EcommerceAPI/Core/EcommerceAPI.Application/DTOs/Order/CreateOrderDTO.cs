﻿using EcommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.DTOs.Order
{
    public class CreateOrderDTO
    {
        public string Description { get; set; }
        public string Address { get; set; }
        public string? BasketId { get; set; }

    }
}

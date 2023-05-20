﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Features.Queries.Basket.GetBasketItems
{
    public class GetBasketItemsQueryRequest:IRequest<List<GetBasketItemsQueryResponse>>
    {
    }
}

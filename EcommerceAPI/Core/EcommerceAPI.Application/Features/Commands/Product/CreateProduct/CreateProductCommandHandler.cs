using EcommerceAPI.Application.Abstractions.Hubs;
using EcommerceAPI.Application.Repositories.Product;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductHubService productHubService;
        private readonly ILogger<CreateProductCommandHandler> logger;

        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductHubService productHubService,ILogger<CreateProductCommandHandler> logger)
        {
            _productWriteRepository = productWriteRepository;
            this.productHubService = productHubService;
            this.logger = logger;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock,
            });
            await _productWriteRepository.SaveAsync();
            await productHubService.ProductAddedMessageAsync($"A product named {request.Name} has been added");
            logger.LogInformation("Product added");
            return new();
        }
    }
}

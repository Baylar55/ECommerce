using EcommerceAPI.Application.Abstractions.Storage;
using EcommerceAPI.Application.Features.Commands.CreateProduct;
using EcommerceAPI.Application.Features.Queries.GetAllProduct;
using EcommerceAPI.Application.Repositories;
using EcommerceAPI.Application.Repositories.File;
using EcommerceAPI.Application.Repositories.InvoiceFile;
using EcommerceAPI.Application.Repositories.Product;
using EcommerceAPI.Application.RequestParameters;
using EcommerceAPI.Application.ViewModels.Product;
using EcommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EcommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IFileReadRepository _fileReadRepository;
        private readonly IFileWriteRepository _fileWriteRepository;
        private readonly IProductImageFileReadRepository _productImageFileReadRepository;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        private readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
        private readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;
        private readonly IStorageService _storageService;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;

        public ProductController(IProductReadRepository productReadRepository,
                                 IProductWriteRepository productWriteRepository,
                                 IFileReadRepository fileReadRepository,
                                 IFileWriteRepository fileWriteRepository,
                                 IProductImageFileReadRepository productImageFileReadRepository,
                                 IProductImageFileWriteRepository productImageFileWriteRepository,
                                 IInvoiceFileReadRepository invoiceFileReadRepository,
                                 IInvoiceFileWriteRepository invoiceFileWriteRepository,
                                 IStorageService storageService,
                                 IConfiguration configuration, 
                                 IMediator mediator)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _fileReadRepository = fileReadRepository;
            _fileWriteRepository = fileWriteRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
            _storageService = storageService;
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }

        //[HttpPut]
        //public async Task<IActionResult> Put(ProductUpdateVM model)
        //{
        //    Product product = await _productReadRepository.GetByIdAsync(model.Id);
        //    product.Price = model.Price;
        //    product.Stock = model.Stock;
        //    product.Name = model.Name;
        //    await _productWriteRepository.SaveAsync();
        //    return Ok();
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(string id)
        {
            List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("product-images", Request.Form.Files);

            Product product = await _productReadRepository.GetByIdAsync(id);

            await _productImageFileWriteRepository.AddRangeAsync(result.Select(r => new ProductImageFile
            {
                FileName = r.fileName,
                Path = r.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<Product>() { product }
            }).ToList());

            await _productImageFileWriteRepository.SaveAsync();

            return Ok();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages(string id)
        {
            Product product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));
            return Ok(product.ProductImageFiles.Select(p => new
            {
                Path = $"{_configuration["BaseStorageUrl"]}/{p.Path}",
                p.FileName,
                p.Id
            }));
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteProductImage(string id, string imageId)
        {
            Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));
            ProductImageFile? productImageFile = product.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(imageId));

            product.ProductImageFiles.Remove(productImageFile);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
    }
}

using EcommerceAPI.Application.Abstractions.Storage;
using EcommerceAPI.Application.Repositories;
using EcommerceAPI.Application.Repositories.File;
using EcommerceAPI.Application.Repositories.InvoiceFile;
using EcommerceAPI.Application.Repositories.Product;
using EcommerceAPI.Application.RequestParameters;
using EcommerceAPI.Application.ViewModels.Product;
using EcommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
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

        public ProductController(IProductReadRepository productReadRepository,
                                 IProductWriteRepository productWriteRepository,
                                 IFileReadRepository fileReadRepository,
                                 IFileWriteRepository fileWriteRepository,
                                 IProductImageFileReadRepository productImageFileReadRepository,
                                 IProductImageFileWriteRepository productImageFileWriteRepository,
                                 IInvoiceFileReadRepository invoiceFileReadRepository,
                                 IInvoiceFileWriteRepository invoiceFileWriteRepository,
                                 IStorageService storageService)
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
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            var totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.CreatedDate,
                p.UpdatedDate
            }).Skip(pagination.Page * pagination.Size).Take(pagination.Size);

            return Ok(new
            {
                totalCount,
                products
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductCreateVM model)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock,
            });
            await _productWriteRepository.SaveAsync();
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
        public async Task<IActionResult> Upload()
        {
            var datas = await _storageService.UploadAsync("files", Request.Form.Files);
            await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile()
            {
                FileName = d.fileName,
                Path = d.pathOrContainerName,
                Storage = _storageService.StorageName
            }).ToList());

            await _productImageFileWriteRepository.SaveAsync();

            return Ok();
        }
    }
}

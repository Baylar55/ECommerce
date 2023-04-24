using EcommerceAPI.Application.Repositories.Product;
using EcommerceAPI.Application.RequestParameters;
using EcommerceAPI.Application.Services;
using EcommerceAPI.Application.ViewModels.Product;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Persistence.Repositories.Product;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;

        public ProductController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IWebHostEnvironment webHostEnvironment, IFileService fileService)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
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

            await _fileService.UploadAsync("resource/product-images", Request.Form.Files);
            return Ok();
        }
    }
}

using EcommerceAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Features.Commands.ProductImageFile.ChangeShowcaseImage
{
    public class ChangeShowcaseImageCommandHandler : IRequestHandler<ChangeShowcaseImageCommandRequest, ChangeShowcaseImageCommandResponse>
    {
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        public ChangeShowcaseImageCommandHandler(IProductImageFileWriteRepository productImageFileReadRepository)
        {
            _productImageFileWriteRepository = productImageFileReadRepository;
        }

        public async Task<ChangeShowcaseImageCommandResponse> Handle(ChangeShowcaseImageCommandRequest request, CancellationToken cancellationToken)
        {
            var query = _productImageFileWriteRepository.Table
                                           .Include(p => p.Products)
                                           .SelectMany(p => p.Products, (productImageFile, p) => new
                                           {
                                               productImageFile,
                                               p
                                           });
            var data = await query.FirstOrDefaultAsync(p => p.p.Id == Guid.Parse(request.ProductId) && p.productImageFile.Showcase);

            if (data != null)
                data.productImageFile.Showcase = false;

            var image = await query.FirstOrDefaultAsync(p => p.productImageFile.Id == Guid.Parse(request.ImageId));
            if (image != null)
                image.productImageFile.Showcase = true;

            await _productImageFileWriteRepository.SaveAsync();

            return new() { };
        }
    }
}

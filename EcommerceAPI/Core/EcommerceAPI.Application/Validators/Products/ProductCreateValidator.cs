using EcommerceAPI.Application.ViewModels.Product;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Validators.Products
{
    public class ProductCreateValidator : AbstractValidator<ProductCreateVM>
    {
        public ProductCreateValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Product name can't be null")
                .MaximumLength(150)
                .MinimumLength(5)
                    .WithMessage("Product name should be between 5 and 150 characters");

            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Stock can't be null")
                .Must(s => s >= 0)
                    .WithMessage("Stock count can't be negative");

            RuleFor(p => p.Price)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Price can't be null")
                .Must(s => s >= 0)
                    .WithMessage("Price can't be negative");
        }
    }
}

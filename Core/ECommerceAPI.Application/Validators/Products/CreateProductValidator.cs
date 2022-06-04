using ECommerceAPI.Application.ViewModels.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {

            RuleFor(product => product.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen ürün adını boş geçmeyiniz.")
                .MaximumLength(150)
                .MinimumLength(3)
                .WithMessage("Lütfen ürün adını 3 ile 150 karakter arasında giriniz.");

            RuleFor(product => product.Stock)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen stok bilgisini boş geçmeyiniz.")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stok bilgisi negatif olamaz.");

            RuleFor(product => product.Price)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lüfen fiyat bilgisini boş geçmeyiniz.")
                .GreaterThan(0)
                .WithMessage("Fiyat bilgisi negatif olamaz.");
        }
    }
}

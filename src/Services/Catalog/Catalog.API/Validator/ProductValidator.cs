using Catalog.API.Entities;
using FluentValidation;

namespace Catalog.API.Validator
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.ProductCode).NotNull().NotEmpty().WithMessage("Please specify a product code.");
            RuleFor(x => x.Price).NotNull().NotEmpty().WithMessage("Please specify a price.");
            RuleFor(x => x.Stock).NotNull().NotEmpty().WithMessage("Please specify a stock.");

           
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.Stock).GreaterThanOrEqualTo(0);


        }
    }
}

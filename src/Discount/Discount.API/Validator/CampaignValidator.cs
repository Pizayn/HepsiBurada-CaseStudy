using Discount.API.Entities;
using FluentValidation;

namespace Discount.API.Profile
{
    public class CampaignValidator : AbstractValidator<Campaign>
    {
        public CampaignValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Please specify a name.");
            RuleFor(x => x.Name).Length(1, 250);
            RuleFor(x => x.ProductCode).NotNull().NotEmpty().WithMessage("Please specify a product code.");
            RuleFor(x => x.Duration).NotNull().NotEmpty();
            RuleFor(x => x.Duration).InclusiveBetween(1, 24);

            RuleFor(x => x.TargetSalesCount).NotNull();
            RuleFor(x => x.PriceManipulationLimit).NotNull().NotEmpty();
            RuleFor(x => x.Duration).InclusiveBetween(1, 99);


        }
    }
}

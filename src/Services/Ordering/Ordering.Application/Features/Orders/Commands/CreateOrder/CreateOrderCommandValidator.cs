using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(p => p.ProductCode)
                .NotEmpty().WithMessage("{ProductCode} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{ProductCode} must not exceed 50 characters.");

            
        }
    }
}

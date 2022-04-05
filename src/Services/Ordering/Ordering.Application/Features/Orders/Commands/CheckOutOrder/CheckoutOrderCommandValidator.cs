using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.CheckOutOrder
{
    public class CheckoutOrderCommandValidator:AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(p=>p.UserName)
                .NotEmpty()
                .WithMessage("{UserName} is required.").NotNull()
                .MaximumLength(50).WithMessage("{UserName} must not exceed the 50 characters")
                .MinimumLength(6).WithMessage("{UserName} must be more than 6 characters");

            RuleFor(p => p.EmailAddress)
               .NotEmpty().WithMessage("{EmailAddress} is required.");

            RuleFor(p => p.TotalPrice)
                .NotEmpty().WithMessage("{TotalPrice} is required.")
                .GreaterThan(0).WithMessage("{TotalPrice} should be greater than zero.");
        }
    }
}

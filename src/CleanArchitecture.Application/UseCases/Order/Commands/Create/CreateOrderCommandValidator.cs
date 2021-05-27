using FluentValidation;

namespace CleanArchitecture.Application.UseCases.Order.Commands.Create
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(_ => _.Name).NotNull().NotEmpty();
        }
    }
}
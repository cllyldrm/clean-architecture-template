using FluentValidation;

namespace CleanArchitecture.Application.UseCases.X.Commands.Process
{
    public class ProcessCommandValidator : AbstractValidator<ProcessCommand>
    {
        public ProcessCommandValidator()
        {
            RuleFor(_ => _.Id).NotNull().NotEmpty();
        }
    }
}
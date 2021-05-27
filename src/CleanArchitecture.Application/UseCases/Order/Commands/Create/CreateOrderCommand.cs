using MediatR;

namespace CleanArchitecture.Application.UseCases.Order.Commands.Create
{
    public class CreateOrderCommand : IRequest<Unit>
    {
        public CreateOrderCommand(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces.Services;
using CleanArchitecture.Domain.AggregateRoots.Order;
using MediatR;

namespace CleanArchitecture.Application.UseCases.Order.Commands.Create
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly IDomainEventService _domainEventService;
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IDomainEventService domainEventService, IOrderRepository orderRepository)
        {
            _domainEventService = domainEventService;
            _orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new OrderEntity(request.Name, OrderStatus.Created);
            await _orderRepository.Create(order);
            await _domainEventService.Publish(order);
            return Unit.Value;
        }
    }
}
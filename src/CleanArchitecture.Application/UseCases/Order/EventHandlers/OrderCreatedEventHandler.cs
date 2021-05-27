using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces.Services;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.AggregateRoots.Order;
using CleanArchitecture.Domain.AggregateRoots.Order.Events;
using MediatR;

namespace CleanArchitecture.Application.UseCases.Order.EventHandlers
{
    public class OrderCreatedEventHandler : INotificationHandler<DomainEventNotification<OrderCreatedEvent>>
    {
        private readonly IServiceBusService _serviceBusRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderCreatedEventHandler(IServiceBusService serviceBusRepository, IOrderRepository orderRepository)
        {
            _serviceBusRepository = serviceBusRepository;
            _orderRepository = orderRepository;
        }

        public async Task Handle(DomainEventNotification<OrderCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var orderId = notification.DomainEvent.Id;
            var order = await _orderRepository.GetById(orderId);
            if (order == null) throw new NotFoundException($"Order was not found, Id:{orderId}");
            order.Complete();
            await _serviceBusRepository.Send(new OrderCompletedEvent(order.Id.Value), Constants.Exchanges.OrderCompleted);
        }
    }
}
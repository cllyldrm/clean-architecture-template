using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces.Services;
using CleanArchitecture.Application.UseCases.Order.Commands.Create;
using CleanArchitecture.Domain.AggregateRoots.Order;
using Moq;
using NUnit.Framework;

namespace CleanArchitecture.Application.UnitTests.UseCases.Order.Commands
{
    public class CreateOrderTests
    {
        private readonly CreateOrderCommandHandler _createOrderCommandHandler;
        private readonly Mock<IDomainEventService> _domainEventService;
        private readonly Mock<IOrderRepository> _orderRepository;

        public CreateOrderTests()
        {
            _domainEventService = new Mock<IDomainEventService>();
            _orderRepository = new Mock<IOrderRepository>();
            _createOrderCommandHandler = new CreateOrderCommandHandler(_domainEventService.Object, _orderRepository.Object);
        }

        [Test]
        public async Task WhenEverythingIsOk_ShouldWorkProperly()
        {
            var command = new CreateOrderCommand("name");
            var entity = new OrderEntity(command.Name, OrderStatus.Created);

            _orderRepository.Setup(_ => _.Create(entity));
            _domainEventService.Setup(_ => _.Publish(entity));
            await _createOrderCommandHandler.Handle(command, new CancellationToken());
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Application.UseCases.Order.Models;
using CleanArchitecture.Application.UseCases.Order.Queries.Get;
using CleanArchitecture.Domain.AggregateRoots.Order;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace CleanArchitecture.Application.UnitTests.UseCases.Order.Queries
{
    public class GetOrderTests
    {
        private readonly GetOrderQueryHandler _getOrderQueryHandler;
        private readonly Mock<IOrderRepository> _orderRepository;
        private readonly Mock<IMapper> _mapper;

        public GetOrderTests()
        {
            _orderRepository = new Mock<IOrderRepository>();
            _mapper = new Mock<IMapper>();
            _getOrderQueryHandler = new GetOrderQueryHandler(_orderRepository.Object, _mapper.Object);
        }

        [Test]
        public async Task WhenEverythingIsOk_ShouldWorkProperly()
        {
            var query = new GetOrderQuery(1);
            var entity = new OrderEntity("name", OrderStatus.Created);
            entity.SetId(1);
            _orderRepository.Setup(_ => _.GetById(query.Id)).ReturnsAsync(entity);
            _mapper.Setup(_ => _.Map<OrderDto>(entity)).Returns(new OrderDto
            {
                Id = 1,
                Name = "name",
                Status = "CREATED"
            });

            var result = await _getOrderQueryHandler.Handle(query, new CancellationToken());

            result.Id.Should().Be(entity.Id);
            result.Name.Should().Be(entity.Name);
            result.Status.Should().Be(entity.Status);
        }

        [Test]
        public async Task WhenOrderNotFound_ShouldReturnNull()
        {
            var query = new GetOrderQuery(1);
            var entity = new OrderEntity("name", OrderStatus.Created);
            entity.SetId(1);
            _orderRepository.Setup(_ => _.GetById(query.Id)).ReturnsAsync((OrderEntity) null);
            var result = await _getOrderQueryHandler.Handle(query, new CancellationToken());
            result.Should().BeNull();
        }
    }
}
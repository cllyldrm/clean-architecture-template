using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Application.UseCases.X.Queries.Get;
using CleanArchitecture.Domain.AggregateRoots;
using CleanArchitecture.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace CleanArchitecture.Application.UnitTests.UseCases.X.Queries
{
    public class GetTests
    {
        private readonly GetQueryHandler _getQueryHandler;
        private readonly Mock<IRepository<XAggregateRoot>> _repository;

        public GetTests()
        {
            _repository = new Mock<IRepository<XAggregateRoot>>();
            _getQueryHandler = new GetQueryHandler(_repository.Object);
        }

        [Test]
        public async Task WhenEverythingIsOk_ShouldWorkProperly()
        {
            var query = new GetQuery(Guid.NewGuid());
            var entity = new XAggregateRoot
            {
                Id = Guid.NewGuid(),
                Status = Status.Started
            };

            _repository.Setup(_ => _.Get(query.Id)).ReturnsAsync(entity);

            var result = await _getQueryHandler.Handle(query, new CancellationToken());

            result.Id.Should().Be(entity.Id);
            result.Status.Should().Be(entity.Status);
        }

        [Test]
        public void WhenNotFound_ShouldThrowNotFoundException()
        {
            var query = new GetQuery(Guid.NewGuid());
            _repository.Setup(_ => _.Get(query.Id))
                .ReturnsAsync((XAggregateRoot) null);

            FluentActions.Invoking(() =>
                    _getQueryHandler.Handle(query, new CancellationToken())).Should()
                .Throw<NotFoundException>();
        }
    }
}
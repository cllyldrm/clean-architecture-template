using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Application.Common.Interfaces.Services;
using CleanArchitecture.Application.UseCases.X.Commands.Process;
using CleanArchitecture.Domain.AggregateRoots;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace CleanArchitecture.Application.UnitTests.UseCases.X.Commands
{
    public class ProcessTests
    {
        private readonly ProcessCommandHandler _processCommandHandler;
        private readonly Mock<IRepository<XAggregateRoot>> _repository;
        private readonly Mock<IDomainEventService> _domainEventService;

        public ProcessTests()
        {
            _repository = new Mock<IRepository<XAggregateRoot>>();
            _domainEventService = new Mock<IDomainEventService>();

            _processCommandHandler = new ProcessCommandHandler(_repository.Object, _domainEventService.Object);
        }


        [Test]
        public void WhenNotFound_ShouldThrowNotFoundException()
        {
            var command = new ProcessCommand(Guid.NewGuid());
            _repository.Setup(_ => _.Get(command.Id))
                .ReturnsAsync((XAggregateRoot) null);

            FluentActions.Invoking(() =>
                    _processCommandHandler.Handle(command, new CancellationToken())).Should()
                .Throw<NotFoundException>();
        }

        [Test]
        public void WhenStatusIsDifferentFromStarted_ShouldThrowUnCorrectStatusException()
        {
            var command = new ProcessCommand(Guid.NewGuid());
            var entity = new XAggregateRoot
            {
                Id = command.Id,
                Status = Status.Finished
            };

            _repository.Setup(_ => _.Get(command.Id))
                .ReturnsAsync(entity);

            FluentActions.Invoking(() =>
                    _processCommandHandler.Handle(command, new CancellationToken())).Should()
                .Throw<UnCorrectStatusException>();
        }

        [Test]
        public async Task WhenEverythingIsOk_ShouldWorkProperly()
        {
            var command = new ProcessCommand(Guid.NewGuid());
            var entity = new XAggregateRoot
            {
                Id = Guid.NewGuid(),
                Status = Status.Started
            };

            _repository.Setup(_ => _.Get(command.Id))
                .ReturnsAsync(entity);

            _repository.Setup(_ => _.Update(entity));
            _domainEventService.Setup(_ => _.Publish(entity));

            await _processCommandHandler.Handle(command, new CancellationToken());

            entity.Status.Should().Be(Status.Finished);
        }
    }
}
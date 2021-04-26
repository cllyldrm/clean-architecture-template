using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Application.Common.Interfaces.Services;
using CleanArchitecture.Domain.AggregateRoots;
using CleanArchitecture.Domain.Events;
using CleanArchitecture.Domain.ValueObjects;
using MediatR;

namespace CleanArchitecture.Application.UseCases.X.Commands.Process
{
    public class ProcessCommandHandler : IRequestHandler<ProcessCommand>
    {
        private readonly IRepository<XAggregateRoot> _repository;
        private readonly IDomainEventService _domainEventService;

        public ProcessCommandHandler(IRepository<XAggregateRoot> repository, IDomainEventService domainEventService)
        {
            _repository = repository;
            _domainEventService = domainEventService;
        }

        public async Task<Unit> Handle(ProcessCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.Get(request.Id);
            if (entity == null) throw new NotFoundException(request.Id);

            entity.ValidateStatus(Status.Started);

            entity.Status = Status.Finished;
            entity.DomainEvents.Add(new ProcessFinishedEvent(entity.Id));
            await _repository.Update(entity);
            await _domainEventService.Publish(entity);

            return Unit.Value;
        }
    }
}
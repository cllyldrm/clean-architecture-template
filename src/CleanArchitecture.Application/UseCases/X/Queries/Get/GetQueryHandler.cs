using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Application.UseCases.X.Models;
using CleanArchitecture.Domain.AggregateRoots;
using MediatR;

namespace CleanArchitecture.Application.UseCases.X.Queries.Get
{
    public class GetQueryHandler : IRequestHandler<GetQuery, XDto>
    {
        private readonly IRepository<XAggregateRoot> _repository;

        public GetQueryHandler(IRepository<XAggregateRoot> repository)
        {
            _repository = repository;
        }

        public async Task<XDto> Handle(GetQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.Get(request.Id);
            if (entity == null) throw new NotFoundException(request.Id);

            return new XDto
            {
                Id = entity.Id,
                Status = entity.Status
            };
        }
    }
}
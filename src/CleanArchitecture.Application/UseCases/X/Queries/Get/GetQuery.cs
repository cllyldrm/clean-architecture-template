using System;
using CleanArchitecture.Application.UseCases.X.Models;
using MediatR;

namespace CleanArchitecture.Application.UseCases.X.Queries.Get
{
    public class GetQuery : IRequest<XDto>
    {
        public Guid Id { get; }

        public GetQuery(Guid id)
        {
            Id = id;
        }
    }
}
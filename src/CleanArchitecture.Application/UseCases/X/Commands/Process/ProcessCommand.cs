using System;
using MediatR;

namespace CleanArchitecture.Application.UseCases.X.Commands.Process
{
    public class ProcessCommand : IRequest<Unit>
    {
        public Guid Id { get; }

        public ProcessCommand(Guid id)
        {
            Id = id;
        }
    }
}
using System;
using CleanArchitecture.Domain.Seedwork;

namespace CleanArchitecture.Domain.Events
{
    public class ProcessFinishedEvent : DomainEvent
    {
        public Guid Id { get; }

        public ProcessFinishedEvent(Guid id)
        {
            Id = id;
        }
    }
}
using System;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.Seedwork;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Domain.AggregateRoots
{
    public class XAggregateRoot : AggregateRoot, IEntity
    {
        public Guid Id { get; set; }

        public Status Status { get; set; }

        public void ValidateStatus(Status status)
        {
            if (!Equals(Status, status))
                throw new UnCorrectStatusException(status);
        }
    }
}
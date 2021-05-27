using System;

namespace CleanArchitecture.Domain.Common.Models
{
    public abstract class DomainEvent
    {
        protected DomainEvent()
        {
            CreatedTime = DateTime.UtcNow;
        }

        public bool IsPublished { get; set; }
        public DateTime CreatedTime { get; }
    }
}
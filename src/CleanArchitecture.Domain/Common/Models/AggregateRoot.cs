using System.Collections.Generic;
using Newtonsoft.Json;

namespace CleanArchitecture.Domain.Common.Models
{
    public class AggregateRoot
    {
        [JsonIgnore] public List<DomainEvent> DomainEvents { get; } = new();
    }
}
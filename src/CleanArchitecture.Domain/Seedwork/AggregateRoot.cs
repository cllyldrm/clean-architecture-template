using System.Collections.Generic;
using Newtonsoft.Json;

namespace CleanArchitecture.Domain.Seedwork
{
    public class AggregateRoot
    {
        [JsonIgnore] public List<DomainEvent> DomainEvents { get; private set; } = new List<DomainEvent>();
    }
}
using System;
using Newtonsoft.Json;

namespace CleanArchitecture.Domain.Entities
{
    public interface IEntity
    {
        [JsonProperty(PropertyName = "id")] public Guid Id { get; set; }
    }
}
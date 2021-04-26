using System;

namespace CleanArchitecture.Domain.Entities
{
    public class EntityItem : IEntity
    {
        public Guid Id { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task Add(TEntity entity);

        Task Update(TEntity entity);

        Task<TEntity> Get(Guid id);

        Task<IEnumerable<TEntity>> GetAll();
    }
}
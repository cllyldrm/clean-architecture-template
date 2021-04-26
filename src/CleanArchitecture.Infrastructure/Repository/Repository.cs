using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Domain.Entities;
using Microsoft.Azure.Cosmos;

namespace CleanArchitecture.Infrastructure.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        private readonly Container _container;

        public Repository(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            _container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task Add(TEntity entity)
        {
            await _container.CreateItemAsync(entity, new PartitionKey(entity.Id.ToString()));
        }

        public async Task Update(TEntity entity)
        {
            await _container.UpsertItemAsync(entity, new PartitionKey(entity.Id.ToString()));
        }

        public async Task<TEntity> Get(Guid id)
        {
            try
            {
                var response =
                    await _container.ReadItemAsync<IEntity>(id.ToString(),
                        new PartitionKey(id.ToString()));
                return (TEntity) response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return default;
            }
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var query =
                _container.GetItemQueryIterator<IEntity>(
                    new QueryDefinition("SELECT * FROM c"));
            var results = new List<TEntity>();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange((IEnumerable<TEntity>) response.ToList());
            }

            return results;
        }
    }
}
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CleanArchitecture.Domain.AggregateRoots.Order;
using Dapper;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Infrastructure.Repositories.Order
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DatabaseConfiguration _databaseConfiguration;

        public OrderRepository(IOptions<DatabaseConfiguration> databaseConfiguration)
        {
            _databaseConfiguration = databaseConfiguration.Value;
        }

        public async Task Create(OrderEntity entity)
        {
            await using var connection = new SqlConnection(_databaseConfiguration.ConnectionString);
            var id = await connection.QuerySingleAsync<int>("INSERT INTO Order (Status) VALUES (@Status); SELECT CAST(SCOPE_IDENTITY() as int)",
                new
                {
                    entity.Status.Value
                });

            entity.SetId(id);
        }

        public async Task Update(OrderEntity entity)
        {
            await using var connection = new SqlConnection(_databaseConfiguration.ConnectionString);
            await connection.ExecuteAsync("UPDATE Order SET Status = @status WHERE Id = @Id",
                new
                {
                    entity.Id,
                    status = entity.Status.Value
                });
        }

        public async Task<OrderEntity> GetById(int id)
        {
            await using var connection = new SqlConnection(_databaseConfiguration.ConnectionString);
            return await connection.QueryFirstOrDefaultAsync<OrderEntity>("SELECT * FROM Order WITH (NOLOCK) WHERE Id = @id", new {id});
        }
    }
}
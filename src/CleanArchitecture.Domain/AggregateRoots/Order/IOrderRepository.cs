using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.AggregateRoots.Order
{
    public interface IOrderRepository
    {
        Task Create(OrderEntity entity);
        Task Update(OrderEntity entity);
        Task<OrderEntity> GetById(int id);
    }
}
using CleanArchitecture.Application.UseCases.Order.Models;
using MediatR;

namespace CleanArchitecture.Application.UseCases.Order.Queries.Get
{
    public class GetOrderQuery : IRequest<OrderDto>
    {
        public GetOrderQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
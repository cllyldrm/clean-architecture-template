using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Application.UseCases.Order.Models;
using CleanArchitecture.Domain.AggregateRoots.Order;
using MediatR;

namespace CleanArchitecture.Application.UseCases.Order.Queries.Get
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrderQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderDto> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetById(request.Id);
            return order == null ? null : _mapper.Map<OrderDto>(order);
        }
    }
}
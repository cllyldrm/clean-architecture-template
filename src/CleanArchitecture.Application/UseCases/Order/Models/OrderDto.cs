using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.AggregateRoots.Order;

namespace CleanArchitecture.Application.UseCases.Order.Models
{
    public class OrderDto : IMapFrom<OrderEntity>
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderEntity, OrderDto>()
                .ForMember(d => Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.Value));
        }
    }
}
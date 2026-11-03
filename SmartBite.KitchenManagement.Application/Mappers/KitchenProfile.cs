using AutoMapper;
using SmartBite.KitchenManagement.Application.Dtos;
using SmartBite.KitchenManagement.Domain.Entities;

namespace SmartBite.KitchenManagement.Application.Mappers
{
    public class KitchenProfile : Profile
    {
        public KitchenProfile()
        {
            CreateMap<KitchenDto, Kitchens>().ReverseMap();
        }
    }
}

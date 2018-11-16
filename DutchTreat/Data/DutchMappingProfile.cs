using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;

namespace DutchTreat.Data
{
    // Profile is inside automapper it is a container around any mappings you want to set up
    public class DutchMappingProfile : Profile
    {
        public DutchMappingProfile()
        {
            // using ForMember so mapper knows to set the order id as the right id
            CreateMap<Order, OrderViewModel>()
                .ForMember(o => o.OrderId, ex => ex.MapFrom(o => o.Id))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemViewModel>()
                .ReverseMap();
        }
    }
}

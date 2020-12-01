using System;
using AutoMapper;
using ErpSystem.Models;

namespace ErpSystem.WebApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, FinalizedOrder>();
        }

    }
}

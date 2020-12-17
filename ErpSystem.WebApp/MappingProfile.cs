namespace ErpSystem.WebApp
{
    using AutoMapper;
    using ErpSystem.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Order, FinalizedOrder>();
        }
    }
}

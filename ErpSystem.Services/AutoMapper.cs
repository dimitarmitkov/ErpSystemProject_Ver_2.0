//using System;
//using AutoMapper;
//using ErpSystem.Models;
//using ErpSystem.Services.ViewModels.Delivery;
//using Microsoft.Extensions.DependencyInjection;

//namespace ErpSystem.Services
//{
//    public class AutoMapper : Profile
//    {
//        public AutoMapper()
//        {
//            CreateMap<Order, DeliveryListViewModel>();
//            CreateMap<WarehouseProduct, DeliveryListViewModel>();
//        }

//        public void ConfigureServices(IServiceCollection services)
//        {
//            // Auto Mapper Configurations
//            var mappingConfig = new MapperConfiguration(mc =>
//            {
//                mc.AddProfile(new AutoMapper());
//            });

//            IMapper mapper = mappingConfig.CreateMapper();
//            services.AddSingleton(mapper);
//        }
//    }
//}

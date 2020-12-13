using AutoMapper;
using ErpSystem.Models;
using ErpSystem.Services.ViewModels.Customer;
using ErpSystem.Services.ViewModels.Supplier;

namespace ErpSystem.Services
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<AddSupplierViewModel, Supplier>();
            CreateMap<Supplier, AddSupplierViewModel>();
            CreateMap<Order, FinalizedOrder>();
            CreateMap<CustomerViewModel, Customer>();
        }
    }
}

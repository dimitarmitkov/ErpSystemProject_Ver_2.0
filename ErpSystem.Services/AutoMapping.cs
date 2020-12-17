namespace ErpSystem.Services
{
    using AutoMapper;
    using ErpSystem.Models;
    using ErpSystem.Services.ViewModels.Customer;
    using ErpSystem.Services.ViewModels.Supplier;

    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            this.CreateMap<AddSupplierViewModel, Supplier>();
            this.CreateMap<Supplier, AddSupplierViewModel>();
            this.CreateMap<Order, FinalizedOrder>();
            this.CreateMap<CustomerViewModel, Customer>();
        }
    }
}

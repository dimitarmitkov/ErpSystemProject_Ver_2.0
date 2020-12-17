namespace ErpSystem.Tests
{
    using AutoMapper;
    using ErpSystem.Data;
    using ErpSystem.Services;
    using ErpSystem.Services.Services;
    using ErpSystem.Services.ViewModels.Supplier;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class CreateSupplierUnitTest
    {
        [Fact]
        public void CheckCorrectCreateSupplierServiceMethodAddSupplier()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ErpSystemDbContext>().UseInMemoryDatabase("testCreateSupplier");
            var dbContext = new ErpSystemDbContext(optionsBuilder.Options);

            var supplier = new AddSupplierViewModel
            {
                SupplierName = "First",
                SupplierCountry = "Germany",
                SupplierAddress = "Berlin",
                SupplierPostalCode = "10020",
                Email = "email@mail.com",
                PhoneNumber = "359 798 800 800",
                CustomsAuthorisationNeeded = false,
                SupplierAdditionalInformation = "none",
            };

            // auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });
            var mapper = mockMapper.CreateMapper();

            var service = new SuppliersService(dbContext, mapper);

            var result = service.AddSupplier(supplier);

            Assert.True(result.IsCompleted);
            Assert.True(result.IsCompletedSuccessfully);
            Assert.False(result.IsFaulted);
        }
    }
}
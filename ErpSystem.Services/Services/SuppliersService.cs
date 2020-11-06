using System;
using ErpSystem.Data;
using ErpSystem.Models;
using ErpSystem.Services.ViewModels.Supplier;

namespace ErpSystem.Services.Services
{
    public class SuppliersService : ISuppliersService
    {
        private readonly ErpSystemDbContext dbContext;

        public SuppliersService(ErpSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddSupplier(AddSupplierViewModel addSupplier)
        {
            var supplier = new Supplier
            {
                SupplierName = addSupplier.SupplierName,
                SupplierPostalCode = addSupplier.SupplierPostalCode,
                SupplierAddress = addSupplier.SupplierAddress,
                SupplierCountry = addSupplier.SupplierCountry,
                Email = addSupplier.Email,
                PhoneNumber = addSupplier.PhoneNumber,
                SupplierAdditionalInformation = addSupplier.SupplierAdditionalInformation,
                CustomsAuthorisationNeeded = addSupplier.CustomsAuthorisationNeeded,
            };

            this.dbContext.Suppliers.Add(supplier);
            this.dbContext.SaveChanges();
        }
    }
}

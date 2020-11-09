using System.ComponentModel.DataAnnotations;
using System.Linq;
using ErpSystem.Data;
using Microsoft.EntityFrameworkCore.Internal;

namespace ErpSystem.Services.ViewModels.Product
{
    public class HasSupplierAttribute : ValidationAttribute
    {
        public HasSupplierAttribute(string supplier)
        {
            this.supplierName = supplier;
        }

        public string supplierName { get; }

        //public string GetErrorMessage() =>
        //    $"Supplier {supplierName} does not exist. Please add Supllier to database.";

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            using (var dbContext = new ErpSystemDbContext())
            {
                if (!dbContext.Suppliers.Any(s => s.SupplierName == value.ToString()))
                {
                    return new ValidationResult($"Supplier {value.ToString()} does not exist. Please add {value} to database.");
                }
            }
            return ValidationResult.Success;
        }
    }
}

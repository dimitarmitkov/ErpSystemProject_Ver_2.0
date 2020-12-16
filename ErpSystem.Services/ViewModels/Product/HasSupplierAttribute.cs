namespace ErpSystem.Services.ViewModels.Product
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using ErpSystem.Data;

    using Microsoft.EntityFrameworkCore.Internal;

    public class HasSupplierAttribute : ValidationAttribute
    {
        public HasSupplierAttribute(string supplier)
        {
            this.supplierName = supplier;
        }

        public string supplierName { get; }

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            using (var dbContext = new ErpSystemDbContext())
            {
                if (value == null)
                {
                    return new ValidationResult("Please insert supplier name");
                }
                else if (!dbContext.Suppliers.Any(s => s.SupplierName == value.ToString()))
                {
                    return new ValidationResult($"Supplier {value.ToString()} does not exist. Please add {value} to database.");
                }
            }

            return ValidationResult.Success;
        }
    }
}

namespace ErpSystem.Services.ViewModels.Product
{
    using System.ComponentModel.DataAnnotations;

    public class CompareDatesAttribute : ValidationAttribute
    {
        public CompareDatesAttribute(string productionDate, string expireDate)
        {
            this.ProductionDate = productionDate;
            this.ExpireDate = expireDate;
        }

        public string ProductionDate { get; }

        public string ExpireDate { get; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // perform comparison
            if (value == null)
            {
                return new ValidationResult("You need to add both Production and Expire dates.");
            }

            return ValidationResult.Success;
        }
    }
}

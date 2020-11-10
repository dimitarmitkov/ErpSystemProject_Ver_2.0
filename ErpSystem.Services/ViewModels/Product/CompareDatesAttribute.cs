using System;
using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Services.ViewModels.Product
{

    public class CompareDatesAttribute : ValidationAttribute
    {
        public CompareDatesAttribute(string productionDate, string expireDate)
        {
            ProductionDate = productionDate;
            ExpireDate = expireDate;
        }

        public string ProductionDate { get; }
        public string ExpireDate { get; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // get your StartDate and EndDate from model and value

            // perform comparison
            if (value == null)
            {
                return new ValidationResult("You need to add both Production and Expire dates.");
            }
            return ValidationResult.Success;
        }
    }
}

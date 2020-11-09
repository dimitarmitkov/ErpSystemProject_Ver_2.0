using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Services.ViewModels.Product
{
    public class CreateProductViewModel
    {

        public string ProductIndentificationNumber { get; set; }

        [Required(ErrorMessage = "Please insert product name")]
        [MinLength(3, ErrorMessage = " Priduct name ought to be at least 3 characters")]
        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        [Required(ErrorMessage = "Please insert product landed price")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Price must be positive value")]
        public decimal ProductLendedPrice { get; set; }

        [Required(ErrorMessage = "Please insert product gross margin needed")]
        [Range(0, 100, ErrorMessage = "Gross margin must be between 0 and 100")]
        public int ProductGrossMargin { get; set; }


        public string Supplier { get; set; }

        [Required(ErrorMessage = "Please insert order days")]
        public int TimeToOrder { get; set; }

        [Required(ErrorMessage = "Please insert delivery days")]
        public int TimeToDelivery { get; set; }

        public string ProductionDate { get; set; }

        public string ExpireDate { get; set; }

        [Required(ErrorMessage = "Please insert transport package type")]
        public string ProductTransportPackage { get; set; }

        [Required(ErrorMessage = "Please insert measurement type")]
        public string MeasurmentTag { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "The field Is Pallet must be checked.")]
        public string IsPallet { get; set; }

        [Display(Name = "Package Width in cm")]
        [Range(1, 200, ErrorMessage = "Width must be between 1 and 200 cm")]
        public int ProductTransportPackageWidthSize { get; set; }

        [Display(Name = "Package Length in cm")]
        [Range(1, 200, ErrorMessage = "Length must be between 1 and 200 cm")]
        public int ProductTransportPackageLengthSize { get; set; }

        [Display(Name = "Package Height in cm")]
        [Range(1, 180, ErrorMessage = "Height must be between 1 and 180 cm")]
        public int ProductTransportPackageHeightSize { get; set; }

        [Display(Name = "Package Weight in kg")]
        [Range(0.1, 500, ErrorMessage = "Weight must be between 0.1 and 500 kg")]
        public int ProductTransportPackageWeight { get; set; }

        [Display(Name = "Pieces per box")]
        [Range(1, 100, ErrorMessage = "Pieces number must be between 1 and 100")]
        public int ProductTransportPackageNumberOfPieces { get; set; }

        [Display(Name = "Boxes per pallet")]
        [Range(1, 60, ErrorMessage = "Boxes number must be between 1 and 60")]
        public int BoxesPerPallet { get; set; }

        [Display(Name = "Sigle product size")]
        public string SingleProductSize { get; set; }

        [Display(Name = "Product description")]
        public string ProductDescription { get; set; }
    }
}

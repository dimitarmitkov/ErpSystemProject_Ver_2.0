using System;
namespace ErpSystem.Services.ViewModels.Product
{
    public class CreateProductViewModel
    {
        public CreateProductViewModel()
        {
        }

        public string productIndentificationNumber { get; set; }
        public string productName { get; set; }
        public decimal productPrice { get; set; }
        public decimal productLendedPrice { get; set; }
        public int productGrossMargin { get; set; }
        public string supplier { get; set; }
        public int timeToOrder { get; set; }
        public int timeToDelivery { get; set; }
        public string productionDate { get; set; }
        public string expireDate { get; set; }
        public string productTransportPackage { get; set; }
        public string measurmentTag { get; set; }
        public string isPallet { get; set; }
        public int productTransportPackageWidthSize { get; set; }
        public int productTransportPackageLengthSize { get; set; }
        public int productTransportPackageHeightSize { get; set; }
        public int productTransportPackageWeight { get; set; }
        public int productTransportPackageNumberOfPieces { get; set; }
        public int boxesPerPallet { get; set; }
        public string singleProductSize { get; set; }
        public string productDescription { get; set; }
    }
}

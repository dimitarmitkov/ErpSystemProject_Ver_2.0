using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Models
{
    public class Product
    {
        public Product()
        {
            this.Sales = new HashSet<Sale>();
            this.Warehouses = new HashSet<Warehouse>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string ProductIndentificationNumber { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public decimal ProductLandedPrice { get; set; }

        public int? ProductDiscount { get; set; }

        public decimal ProductSalePrice { get; set; }

        [Required]
        [Range(0, 99)]
        public int ProductGrossMargin { get; set; }

        public virtual Supplier Supplier { get; set; }

        [Required]
        [Range(0, 360)]
        public int TimeToOrder { get; set; }

        [Required]
        [Range(0, 360)]
        public int TimeToDelivery { get; set; }

        public DateTime? ProductionDate { get; set; }

        public DateTime? ExpireDate { get; set; }

        public int ProductTransportPackageId { get; set; }

        public virtual TransportPackageTag ProductTransportPackage { get; set; }//Pallet, box, container, ect.

        public int MeasurementId { get; set; }

        public virtual ProductMeasurmentTag MeasurmentTag { get; set; }

        public virtual WarehouseProduct WarehouseProduct { get; set; }

        public bool IsPallet { get; set; }

        [Range(0, 100)]
        public int ProductTransportPackageWidthSize { get; set; }

        [Range(0, 100)]
        public int ProductTransportPackageLengthSize { get; set; }

        [Range(0, 100)]
        public int ProductTransportPackageHeightSize { get; set; }

        [Range(0, 100)]
        public int ProductTransportPackageWeight { get; set; }

        [Range(0, int.MaxValue)]
        public int ProductTransportPackageNumberOfPieces { get; set; }

        [Range(0, int.MaxValue)]
        public int BoxesPerPallet { get; set; }

        public string SingleProductSize { get; set; }

        public string ProductDescription { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }

        public virtual ICollection<Warehouse> Warehouses { get; set; }
    }
}

namespace ErpSysytem.ConsoleApp
{
    using AutoMapper;
    using ErpSystem.Data;
    using ErpSystem.Models;
    using ErpSystem.Services.ViewModels.Delivery;
    using Microsoft.EntityFrameworkCore;

    public class ConsoleStartUp
    {
        public static void Main(string[] args)
        {
            var db = new ErpSystemDbContext();

            // db.Database.EnsureDeleted();
            // db.Database.EnsureCreated();

            db.Database.Migrate();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                //mc.AddProfile(new MappingProfile());
                mc.CreateMap<Order, DeliveryListViewModel>();
            });

            IMapper mapper = mapperConfig.CreateMapper();


            //IProductsService productService = new ProductsService(db);

            //ICustomersService customerService = new CustomersService(db);

            //IWarehousesService warehousesService = new WarehousesService(db);

            //ISuppliersService suppliersService = new SuppliersService(db);

            //ISalesService salesService = new SalesService(db);

            //IUsersService usersService = new UsersService(db);

            // AddSupplierViewModel addSupplier1 = new AddSupplierViewModel
            // {
            //    SupplierName = "DotNet Gmbh",
            //    SupplierAddress = "Berlin",
            //    SupplierCountry = "Germany",
            //    SupplierPostalCode = "10115",
            //    CustomsAuthorisationNeeded = false,
            //    Email = "office@dotnetgmbh.com",
            //    PhoneNumber = "+4930 1234567",
            //    SupplierAdditionalInformation = "DotNet Gmbh initial information",
            //};

            //suppliersService.AddSupplier(addSupplier1);

            //AddSupplierViewModel addSupplier2 = new AddSupplierViewModel
            //{
            //    SupplierName = "AspNet Gmbh",
            //    SupplierAddress = "Muenchen",
            //    SupplierCountry = "Germany",
            //    SupplierPostalCode = "80331",
            //    CustomsAuthorisationNeeded = true,
            //    Email = "office@aspnetgmbh.com",
            //    PhoneNumber = "+4989 1234567",
            //    SupplierAdditionalInformation = "DotNet Gmbh initial information",
            //};

            //suppliersService.AddSupplier(addSupplier2);

            //CreateProductViewModel createProduct5 = new CreateProductViewModel
            //{
            //    productName = "Fifth Product",
            //    productLendedPrice = 9.08m,
            //    productDescription = "first product added",
            //    productionDate = DateTime.UtcNow.ToString(),
            //    expireDate = DateTime.UtcNow.ToString(),
            //    supplier = "DotNet Gmbh",
            //    singleProductSize = "120/80/180",
            //    boxesPerPallet = 10,
            //    productTransportPackageWeight = 400,
            //    productGrossMargin = 10,
            //    productIndentificationNumber = "007",
            //    productTransportPackage = "Pallet",
            //    productTransportPackageWidthSize = 80,
            //    productTransportPackageLengthSize = 120,
            //    productTransportPackageHeightSize = 180,
            //    productTransportPackageNumberOfPieces = 2,
            //    isPallet = "true",
            //    measurmentTag = "Kg",
            //    timeToDelivery = 10,
            //    timeToOrder = 10,
            //};

            //productService.CreateProduct(createProduct5);

            //CreateProductViewModel createProduct2 = new CreateProductViewModel
            //{
            //    productName = "Second Product",
            //    productPrice = 100.99m,
            //    productDescription = "second product added",
            //    productionDate = DateTime.UtcNow.ToString(),
            //    expireDate = DateTime.UtcNow.ToString(),
            //    supplier = "DotNet Gmbh",
            //    singleProductSize = "10/30/20",
            //    boxesPerPallet = 100,
            //    productTransportPackageWeight = 4,
            //    productGrossMargin = 7,
            //    productIndentificationNumber = "008",
            //    productTransportPackage = "Box",
            //    productTransportPackageWidthSize = 10,
            //    productTransportPackageLengthSize = 30,
            //    productTransportPackageHeightSize = 20,
            //    productTransportPackageNumberOfPieces = 90,
            //    isPallet = "false",
            //    measurmentTag = "Pieces",
            //    timeToDelivery = 20,
            //    timeToOrder = 70,
            //};

            //productService.CreateProduct(createProduct2);

            //CreateProductViewModel createProduct3 = new CreateProductViewModel
            //{
            //    productName = "Second Product",
            //    productPrice = 200.99m,
            //    productDescription = "third product added",
            //    productionDate = DateTime.UtcNow.ToString(),
            //    expireDate = DateTime.UtcNow.ToString(),
            //    supplier = "DotNet Gmbh",
            //    singleProductSize = "10/30/10",
            //    boxesPerPallet = 100,
            //    productTransportPackageWeight = 4,
            //    productGrossMargin = 7,
            //    productIndentificationNumber = "009",
            //    productTransportPackage = "Bottle",
            //    productTransportPackageWidthSize = 10,
            //    productTransportPackageLengthSize = 30,
            //    productTransportPackageHeightSize = 10,
            //    productTransportPackageNumberOfPieces = 40,
            //    isPallet = "false",
            //    measurmentTag = "Liter",
            //    timeToDelivery = 10,
            //    timeToOrder = 7,
            //};

            //productService.CreateProduct(createProduct3);

            //CreateProductViewModel createProduct4 = new CreateProductViewModel
            //{
            //    productName = "Fourth Product",
            //    productPrice = 1100.45m,
            //    productDescription = "fourth product added",
            //    productionDate = DateTime.UtcNow.ToString(),
            //    expireDate = DateTime.UtcNow.ToString(),
            //    supplier = "AspNet Gmbh",
            //    singleProductSize = "100/80/120",
            //    boxesPerPallet = 1,
            //    productTransportPackageWeight = 1200,
            //    productGrossMargin = 12,
            //    productIndentificationNumber = "011",
            //    productTransportPackage = "One piece",
            //    productTransportPackageWidthSize = 100,
            //    productTransportPackageLengthSize = 80,
            //    productTransportPackageHeightSize = 120,
            //    productTransportPackageNumberOfPieces = 1,
            //    isPallet = "true",
            //    measurmentTag = "Tons",
            //    timeToDelivery = 7,
            //    timeToOrder = 14,
            //};

            //productService.CreateProduct(createProduct4);

            //CustomerViewModel customer1 = new CustomerViewModel
            //{
            //    CompanyName = "Customer 1",
            //    City = "Sofia",
            //    CompanyType = "EOOD",
            //    CustomerDiscount = 10,
            //    PostalCode = 1000,
            //    Address = "Center",
            //    Email = "company1@email.com",
            //    HasDelivery = true,
            //    IsActive = true,
            //    PhoneNumber = "+359 88 8243111",
            //    AdditionalInfo = "first added customer"
            //};

            //customerService.CreateCustomer(customer1);


            //CustomerViewModel customer2 = new CustomerViewModel
            //{
            //    CompanyName = "Customer 4",
            //    City = "Sofia",
            //    CompanyType = "AD",
            //    CustomerDiscount = 0,
            //    PostalCode = 1101,
            //    Address = "Stamboliyski",
            //    Email = "comp4any@email.com",
            //    HasDelivery = true,
            //    IsActive = false,
            //    PhoneNumber = "+359 88 8243222",
            //    AdditionalInfo = "second added customer"
            //};

            //customerService.CreateCustomer(customer2);

            //CustomerViewModel customer3 = new CustomerViewModel
            //{
            //    CompanyName = "Customer 3",
            //    City = "Sofia",
            //    CompanyType = "EAD",
            //    CustomerDiscount = 5,
            //    PostalCode = 1010,
            //    Address = "Stamboliyski",
            //    Email = "company3@email.com",
            //    HasDelivery = false,
            //    IsActive = false,
            //    PhoneNumber = "+359 88 8243333",
            //    AdditionalInfo = "third added customer"
            //};

            //customerService.CreateCustomer(customer3);

            //CustomerViewModel customer4 = new CustomerViewModel
            //{
            //    CompanyName = "Customer 4",
            //    City = "Sofia",
            //    CompanyType = "OOD",
            //    CustomerDiscount = 20,
            //    PostalCode = 1303,
            //    Address = "Zona",
            //    Email = "company4@email.com",
            //    HasDelivery = false,
            //    IsActive = true,
            //    PhoneNumber = "+359 88 824444",
            //    AdditionalInfo = "fourth added customer"
            //};

            //customerService.CreateCustomer(customer3);


            //var resultProductsName = productService.SearchByProductNameAndId(null, null);

            //foreach (var resultProduct in resultProductsName)
            //{
            //    Console.WriteLine($"Product: {resultProduct.ProductName}, Price: {resultProduct.ProductSalePrice}, Total Available Products: {resultProduct.ProductsAvailable}, Total Products Delivery Price = {resultProduct.ProductSalePrice}, LND: {resultProduct.ProductLandedPrice}");
            //}

            //var resultProductsPrice = productService.SearchByProductPrice(100, null);

            //foreach (var product in resultProductsPrice)
            //{
            //    Console.WriteLine($"Product: {product.ProductName} {product.MeasurmentTag}, Price: {product.ProductSalePrice}, Transport: {product.ProductTransportPackage}, Supplier: {product.Supplier}");
            //}

            //var resultProductsSupplierLocation = productService.SearchByProductSupplierCountryOrCity("Germany", null);

            //foreach (var location in resultProductsSupplierLocation)
            //{
            //    Console.WriteLine($"Product: {location.ProductName} {location.MeasurmentTag}, Supplier: {location.Supplier}");
            //}

            //customerService.DeleteCustomer("1c4920df-031b-45b0-95c2-f841d09689f7", null);

            //var resultCustomerByCodeOrAddress = customerService.SearchByCustomerNamePostalCodeAndAddress(null, null, null);

            //foreach (var location in resultCustomerByCodeOrAddress)
            //{
            //    Console.WriteLine($"Company: {location.CompanyName} {location.CompanyType}, City: {location.City}");
            //}

            //var product008 = new AddProductWaerhouseViewModel
            //{
            //    WarehouseId = 1,
            //    ProductId = 2,
            //    AddQuantity = 18000,
            //    SpaceTaken = 2,
            //};

            //warehousesService.AddProduct(product008);

            //var product2001 = new AddProductWaerhouseViewModel
            //{
            //    WarehouseId = 1,
            //    ProductId = 4,
            //    AddQuantity = 5,
            //    SpaceTaken = 5,
            //    ProductionDate = DateTime.UtcNow,
            //    ExpireDate = DateTime.UtcNow.AddMonths(2),
            //};

            //warehousesService.AddProduct(product2001);

            //var product007 = new AddProductWaerhouseViewModel
            //{
            //    WarehouseId = 1,
            //    ProductId = 1,
            //    AddQuantity = 2,
            //    SpaceTaken = 2,
            //};

            //warehousesService.AddProduct(product007);

            //var product009 = new AddProductWaerhouseViewModel
            //{
            //    WarehouseId = 1,
            //    ProductId = 3,
            //    AddQuantity = 80,
            //    SpaceTaken = 2,
            //};

            //warehousesService.AddProduct(product009);

            //salesService.CreateSale(1, "2fd895b5-3eaf-4ef9-8456-4118bbc79f82", 1, true, true, 1);

            //var product012 = new AddProductWaerhouseViewModel
            //{
            //    WarehouseId = 1,
            //    ProductId = 7,
            //    AddQuantity = 1400,
            //    SpaceTaken = 2,
            //};

            //warehousesService.AddProduct(product012);

            //salesService.CreateSale(7, "4c53ac30-60c2-45ad-91ca-280ebc4490f8", 560, false, false, 1);

            //var product010 = new AddProductWaerhouseViewModel
            //{
            //    WarehouseId = 1,
            //    ProductId = 8,
            //    AddQuantity = 300,
            //    SpaceTaken = 3,
            //};

            //warehousesService.AddProduct(product010);

            //salesService.CreateSale(2, "2fd895b5-3eaf-4ef9-8456-4118bbc79f82", 900, true, true, 1);

            //var sales = salesService.ListOfSales();

            //foreach (var sale in sales)
            //{
            //    Console.WriteLine($"Customer: {sale.Customer}, Product: {sale.Product} => pieces: {sale.NumberOfSoldProducts}; singlr prod price {sale.SingleProudctSalePrice} total: {sale.TotalSalePrice}");
            //}

            //usersService.CreateUser("Dimitar", "Mitkov", "dimitar.mitkov@me.com", "123456");

            //var sales = salesService.TotalSalesPerDate();
            //foreach (var sale in sales)
            //{
            //    Console.WriteLine($"{sale.Key} : {sale.Value}");
            //}
        }
    }
}

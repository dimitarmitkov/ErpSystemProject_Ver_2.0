# ErpSystemProject
SoftUni Project 2020



This is basic ERP system. Entry point is ErpSysytem.ConsoleApp/ConsoleStartup.cs

Main functionalities:

1. Product

1.1. Product Model (ErpSystem.Models/Product.cs)

1.2. Product Services Interface (ErpSystem.Services/Services/IProductsService.cs)

1.3. Product Services (ErpSystem.Services/Services/ProductsService.cs). Main functionalities:

1.3.1. Create product:

- this method takes view model form ErpSystem.Services/ViewModels/Product/CreateProductViewModel.cs;
- if production date is empty it is set to null (it might be in use when production date in not applicable);
- if expire date is empty it is set to null (it might be in use when production date in not applicable);
- supplier settings;
- product transport package setting;
- measurement setting;

1.3.2. Delete Product - deletes Prduct by Id and Product Name;

1.3.3. Search Product - selects Products by Id or Name. If no Name or Id is select - all products are selected.

1.3.4. Search Product by Price - selects Products with prices higher than minPrice, or prices lower than maxPrice, or price between min and max price;

1.3.5. Search Product by Country or City of origin - selects product by country or city. If it is no country neither city was selected - it returns all products;

2. Customer

2.1. Customer Model (ErpSystem.Models/Customer.cs)

2.2. Customer Services Interface (ErpSystem.Services/Services/ICustomersService.cs)

2.3. Customer Services (ErpSystem.Services/Services/CustomersService.cs) Main functionalities:

2.3.1. Create Customer:

- this method takes view model form ErpSystem.Services/ViewModels/Customer/CustomerViewModel.cs;
- customer company type setting;

2.3.2. Delete Customer - deletes Customer by Id or Name. If more than one Name is/are equal - all customers will be deleted;

2.3.3. Search by Customer name, Postal code and Address - returns selection of customers by one of selection option. If no option is choosen - returns all customers;

2.3.4. Search Customer by Phone or Email - returns selection of customers by one of selection option. If no option is choosen - returns all customers;





//TODO Calculate order schedule depending on TransportPackageUnits

//TODO warehouse needed products volume must predict needed products quantities depending on sales quantities compared with products on stock, time to order and time to delivery

// TODO functionality for single product price calculation. Must include single transport unit (box, pallet, container, etc.), transport expenses, products in unit, product measure (weight, pieces, volume, other);

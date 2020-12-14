# ErpSystemProject
SoftUni Project 2020



This is basic ERP system. Entry point is ErpSysytem.WebApp/Program.cs

Main functionalities:

1. Dashboard
1.1. Dashboart shows main functionalities of application.
1.2. On Dashboard are placed buttons for:
  - Create Product;
  - Search Product;
  - Delete Product (visible only for Users with Admin Role);
  - Sales List;
  - Generate Sale;
  - Sales Charts;
  - Order;
  - Delivery;
  - Add Supplier;
  - Add Customer;

2. Side bar inherits same functionalities form Dashboard, exended with additional actions. All finctionalities of Side bar are wrapped in 5 main articles:
  - Dashboard calls Dasboard
  - Actions calls: Products All (all products table); Sales All (all sales table); Delete Product (delete product, it is visible just for demonstrative purpose);
  - References calls: Create Product; Generate Sale; Current Sale (it is visible just for demonstrative purpose); Generate Invoice (it is visible just for demonstrative purpose); Generate Order; Generate Delivery; API Sale (it is visible just for demonstrative purpose);

3. User
  3.1. Regisrer User - registers new user with email and password.
  3.2. Login User - login User in application. Every User could Login form one entry poin only.
  3.3. Logout - logout User from application.

4. User Roles
  4.1. There are 2 types User Roles: User and Admin.
  4.2. Admin is allowed to Delete products. Delete Produc button on Dashboard is visible only when User with Admin Role is logged in.
  
5. Sale
  5.1. Sale culd be initiated be pressing Generate Sale button on Dashboard
  5.2. First step of Sale is to select Customer form drop-down selector. When Customer is selected, after pressing 

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
    
  1.3. On Dashboard is presented information about current available and taken space of Warehouse. It is presented in 2 pie-cahrts: one per pallet space (count of pallet spaces) and one per boxes space (presenting shelf lenght in cm);

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
    
    5.1. Sale culd be initiated be pressing Generate Sale button on Dashboard.
    
    5.2. First step of Sale is to select Customer form drop-down selector. Then choose whether Customer will receive Discount per curren Sale by checkin Has Discount checkbox. When Customer and Discount are selected, by pressing Filter button, Sale process is started for selecter Customer.
    
    5.3. All avavilable products appear for selected Customer and process of sale will continue ater selection of product by pressig green colored Select button on right end of every product row.
    
    5.4. Selected Product appears on sigle row and it needed to be selected if there will be given Product Discount (additional to Customer Discount) by tick in checkbox named Prod Discount. Number of products for sale ought to be typed in Sale Volume box. Only numbers less than or equal to Products Availabe are allowed to be entered. In case of incorrect entered amout or in case of not numeric symbols are typed in box button Sale will not appear. Also Sale button will disapire if number typed exceeds Available Products. Sale for selected product is finalized by pressing Sale button.
    
    5.5. When all products requested by Customer are sold, Sales process ouht to be finalized by pressing Finalize sale for ... customer.
    
    5.6. When Sale is finalized Invoice appears on screen. Invoice contains all needed requisites for Customer and Company, and also list of sold products, discounts information, sales prices after discount, VAT, total amount. By pressing Print invoice button Sales process is finalized and invoice is printed.
    
6. Order
    
    6.1. During Sale process application is checking every Sale by measuring Available Products of every species of Products, time needed form Order start to Delivery end, number of Products sold for that time. In case of possible lack of products in Order/Delivery time horizon during Sale process a meaasge will appear informing the operator that Product need to be ordared and asking for confirmation by operator before continue with Sale process.
    
    
    
    
    

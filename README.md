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
    - Sales Charts - graphic data for sales;
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
    
    5.7. When Sale is over number of Available products is decreased. I case of whole box or pallet is sold Warehouse space is increasing respectivly with number of pallets sold or with width/lenght in centimeters multyplied by number of boxes sold. Boxes take place on shelf accordingly on their width if its lenght is less than shelft depth. If box lenght is bigger that shelf depth the box will be placed with longer side along the shelf, so respectively it will take place equal its lenght.
    
6. Order
    
    6.1. During Sale process application is checking every Sale by measuring Available Products of every species of Products, time needed form Order start to Delivery end and number of Products sold for that time. In case of possible lack of products in Order/Delivery time horizon during Sale process a meaasge will appear informing the User that Product need to be ordared and asking for confirmation by User before continue with Sale process.
    
     6.2. When any Product for order is located, it will be stored in pending for Order Products storage.
     
     6.3. User could check all Order needed Products by click on Order button in Dashboard menu.
     
     6.4. Onclick list of Product needed Order will appear. These product ought to be separated by Supplier, from Supplier drop-down menu. Once Supplier is selected all prpducts delivered by this Supplier are open for order. In Order suggestion form User will find information about: Supplier, Product Id, Product name, EXW price, total delivery days neede, Order total weight, Currently available products, Sales based on delivery time, suggested order volume based on transport package (boxes, pallets, ect.), transport package type. User have to type number of transport units for order and then to end Order for that product by pressing Confirm order button.
     
     6.5. When all Products delivered by selecter Supplier are set for Order, User needs to finalize Order for that product group by pressing Finalize order button. 
     
     7. Delivery
     
     7.1. When Delivery appears in Warehouse, it will be checked by User. Then Delivery ought to be put in stock. First of all User have to choose Deliveru button in Dashboard.
     
     7.2. After selection all Products received for delivery will appear on screen. The information containing request for Delivery contauns: Supplier, Product Id, Product name, Number of transport units as per Order (it could be different, so it must be confirmerd by User), tatal weight of delivery, Product delivery price, total Delivery price, type of transport unit. User must type Production date, Expire date (taken form invoice, or CMR) and real number of delivered transport units.
     
     7.3. Delivery is finalized by click on Confirm delivery button. After all Deliveries are confirmad, User have to finaliza whole Delivery by pressing Finaliz Delivery.
     
     7.4. Finalized Delivery takes spaces from pallet or/and boxes areas in Warehouse.
     
     8. Supplier
     
     8.1. Add Supplier - add new Supllier
     
     9. Customer
     
     9.1. Add Customer - add new Customer
     
     10. Sales Charts - graphic Sales data. It appears after select Sales period.
     
     
   
   
    
    
    

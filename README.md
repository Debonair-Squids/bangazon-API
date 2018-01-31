# Bangazon API
This is an API for Bangazon INC. This API will allow users to GET/POST/PUT/DELETE items from the Bangazon Database.


# Installing Bangazon API

The database is going to be hosted on your local computer.
 1. Clone the repo on to you local machine.
 1. Run `dotnet restore`
 1. Run `dotnet ef migrations add bangazonapi`
 >This will create all the migrations needed for Entity Framework to post items to the database based on the models in the Models/ directory
 4. Run `dotnet ef database update`
 1. Run `dotnet run`
 > This will compile and run everything as well as initializing the database with some data to get started
## Usage

## Cross-Origin Resource Sharing
CORS restrictions were set up to only allow requests from bangazon.com.  To test the restriction, and index.html file is available in the CorsTest directory in this project.  The sample page makes an AJAX request to our API on localhost:5000.  To try the test, follow these steps:

+ Make sure the API is running and listening on `localhost:5000`.
+ In a separate terminal window, open the CorsTest directory, and run `http serve`.
+ In your browser, run that port and check your dev tools console.  There should be a message stating describing a CORS failure.  This will indicate that the request came from a site not authorized, and CORS is working correctly.
+ Shut down that server process.
+ In your terminal, run `sudo vim etc/hosts`.
+ Type `i` to insert text.

***Make your file match the following:***


#### Host Database

localhost is used to configure the loopback interface
when the system is booting.  Do not change this entry.

```
127.0.0.1        localhost
127.0.0.1        www.bangazon.com
127.0.0.1        bangazon.com
255.255.255.255  broadcasthost
::1              localhost
```

### Customer
||Method|Description|
|---|---| ----------|
| List | GET |`http://localhost:5000/Customer`|
| Single | GET |`http://localhost:5000/Customer/{Id}`|
| Edit | PUT |`http://localhost:5000/Customer/{Id}` |

#### Customers With Orders
||Method|Description|
|---|---| ----------|
| List | GET |`http://localhost:5000/Customer/?active=true`|

#### Customers Without Orders
||Method|Description|
|---|---| ----------|
| List | GET |`http://localhost:5000/Customer/?active=false`|


### Product Type
||Method|Description|
|---|---| ----------|
| List | GET |`http://localhost:5000/productType`|
| Single | GET |`http://localhost:5000/productType/{Id}`|
| Edit | PUT |`http://localhost:5000/productType/{Id}`|
| Delete | DELETE |`http://localhost:5000/productType/{Id}`|

### Product
||Method|Description|
|---|---| ----------|
| List | GET |`http://localhost:5000/product`|
| Single | GET |`http://localhost:5000/product/{Id}`|
| Edit | PUT |`http://localhost:5000/product/{Id}`|
| Delete | DELETE |`http://localhost:5000/product/{Id}`|

### Payment Type
||Method|Description|
|---|---| ----------|
| List | GET |`http://localhost:5000/PaymentType`|
| Single | GET |`http://localhost:5000/PaymentType/{Id}`|
| Edit | PUT |`http://localhost:5000/PaymentType/{Id}`|
| Delete | DELETE |`http://localhost:5000/PaymentType/{Id}`|

### Customer Payment
||Method|Description|
|---|---| ----------|
| List | GET |`http://localhost:5000/CustomerPayment`|
| Single | GET |`http://localhost:5000/CustomerPayment/{Id}`|
| Edit | PUT |`http://localhost:5000/CustomerPayment/{Id}`|
| Delete | DELETE |`http://localhost:5000/CustomerPayment/{Id}`|

### Orders
||Method|Description|
|---|---| ----------|
| List | GET |`http://localhost:5000/Orders`|
| Single | GET |`http://localhost:5000/Orders/{Id}`|
| Edit | PUT |`http://localhost:5000/Orders/{Id}`|
| Delete | DELETE |`http://localhost:5000/Orders/{Id}`|

### Department
||Method|Description|
|---|---| ----------|
| List | GET |`http://localhost:5000/Department`|
| Single | GET |`http://localhost:5000/Department/{Id}`|
| Edit | PUT |`http://localhost:5000/Department/{Id}`|
| Delete | DELETE |`http://localhost:5000/Department/{Id}`|

### Employee
||Method|Description|
|---|---| ----------|
| List | GET |`http://localhost:5000/Employee`|
| Single | GET |`http://localhost:5000/Employee/{Id}`|
| Edit | PUT |`http://localhost:5000/Employee/{Id}`|
| Delete | DELETE |`http://localhost:5000/Employee/{Id}`|

### Training
||Method|Description|
|---|---| ----------|
| List | GET |`http://localhost:5000/Training`|
| Single | GET |`http://localhost:5000/Training/{Id}`|
| Edit | PUT |`http://localhost:5000/Training/{Id}`|
| Delete | DELETE |`http://localhost:5000/Training/{Id}`|

### Computer
||Method|Description|
|---|---| ----------|
| List | GET |`http://localhost:5000/Computer`|
| Single | GET |`http://localhost:5000/Computer/{Id}`|
| Edit | PUT |`http://localhost:5000/Computer/{Id}`|
| Delete | DELETE |`http://localhost:5000/Computer/{Id}`|


## Credits

Jenna Solis
https://github.com/Jennabsol

Robert Shock
https://github.com/RobertShock

Jesse Page
https://github.com/JPage4

Erin Agobert
https://github.com/eagobert

Paul Ellis
https://github.com/tynesellis

























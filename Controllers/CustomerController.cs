using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bangazon_inc.Data;
using bangazon_inc.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bangazon_inc.Controllers

{
    //Sets URL route to <websitename>/Customer
    [Route("[controller]")]
    [EnableCors("AllowOnlyBangazonians")]
    //Creates a new Customer controller class that inherits methods from AspNetCore Controller class
    public class CustomerController : Controller
    {
        //Sets up an empty variable _context that will  be a reference of our BangazonAPIContext class
        private BangazonContext _context;
        //Contructor that instantiates a new Customer controller and sets _context equal to a new instance of our BangazonAPIContext class
        public CustomerController(BangazonContext ctx)
        {
            _context = ctx;
        }

        // GET METHOD
        //http://localhost:5000/Customer/ will return a list of all customers. 
        [HttpGet]

        //Get() is a mathod from the AspNetCore Controller class to retreive info from database. 
        public IActionResult Get(bool? active)
        {
            //Check for the active search query
            if(active is bool)
            {
                if(active == true)
                {   
                    //Gets Customers that have placed an order
                    IQueryable<object> customers = from customer in _context.Customer
                                                where _context.Orders.Any(order=>(order.CustomerId == customer.CustomerId))  
                                            select customer;
                    if (customers==null)
                    {
                        return NotFound();
                    }
                    return Ok(customers);
                }
                else if (active == false)
                {
                    //Gets Customer that have NOT placed an order
                    IQueryable<object> customers = from customer in _context.Customer
                                                where !_context.Orders.Any(order=>(order.CustomerId == customer.CustomerId))  
                                            select customer;
                    if (customers == null)
                    {
                        return NotFound();
                    }

                    return Ok(customers);
                }
            }
            if (active == null)
            {
                //Sets a new IQuerable Collection of <objects> that will be filled with each instance of _context.Customer
                IQueryable<object> customers = from customer in _context.Customer select customer;

                //if the collection is empty will retur NotFound and exit the method. 
                if (customers == null)
                {
                    return NotFound();
                }

                //otherwise return list of the customers
                return Ok(customers);
                
            }
            else
            {
                return BadRequest();
            }

        }

        // GET Single Customer
        // http://localhost:5000/Customer/{id} will return info on a single customer based on ID, with theor products nested in the JSON
        [HttpGet("{id}", Name = "GetSingleCustomer")]

        // Will run Get based on the id from the url route. 
        public IActionResult Get([FromRoute] int id)
        {
            // If you request anything other than an Id you will get a return of BadRequest. 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // Builds a custom JSON object for improved readability
                Customer customer = _context.Customer.Single(m => m.CustomerId == id);
                var completedOrders = from o in _context.Orders
                                      where o.PaymentTypeId != null
                                      && o.CustomerId == id
                                      select o;
                CustomerWithOrderJSON custJSON = new CustomerWithOrderJSON()
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    CustomerID = customer.CustomerId
                };
                List <OrderOnCustomerJSON> completedOrdersJSON = new List <OrderOnCustomerJSON>();
                foreach (Order order in completedOrders) {
                    OrderOnCustomerJSON newOrder = new OrderOnCustomerJSON(){
                        OrderID = order.OrderID,
                        CustomerID = order.CustomerID,
                        PaymentTypeID = order.PaymentTypeID,
                        DateCreated = order.DateCreated
                    };
                    completedOrdersJSON.Add(newOrder);
                }
                custJSON.Orders = completedOrdersJSON;
                if (customer == null)
                {
                    return NotFound();
                }
                return Ok(custJSON);
            }
            //if the try statement fails for some reason, will return error of what happened. 
            catch (System.InvalidOperationException ex)
            {
                return NotFound(ex);
            }
        }

        // POST
        // //http://localhost:5000/Customer/ will post new customer to the DB 
        [HttpPost]
        //takes the format of Customer type as a JSON format and adds to database. 
        public IActionResult Post([FromBody] Customer newCustomer)
        {
            //Checks to make sure model state is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Will add new customer to the context
            //This will not yet be added to DB until .SaveChanges() is run
            _context.Customer.Add(newCustomer);
            

            //Will attempt to save the changes to the DB.
            //If there is an error, will throw exception code. 
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                //this checks to see if a new customer we are trying to add has a CustomerID that already exists in the system
                if (CustomerExists(newCustomer.CustomerID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            //if everything successfull, will run the "GetSingleCustomer" method while passing the new ID that was created and return the new Customer
            return CreatedAtRoute("GetSingleCustomer", new { id = newCustomer.CustomerID }, newCustomer);
        }


        //Helper method to check to see if a CustomerID is already in the system
        private bool CustomerExists(int customerId)
        {
          return _context.Customer.Count(e => e.CustomerID == customerId) > 0;
        }

        // PUT 
         //http://localhost:5000/Customer/{id} will edit a customer entry in the DB.  
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Customer modifiedCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != modifiedCustomer.CustomerID)
            {
                return BadRequest();
            }

            _context.Entry(modifiedCustomer).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }

       
    }
}
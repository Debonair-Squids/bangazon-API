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
    [Route("[controller]")]
    // [EnableCors("AllowBangazonEmployeesOnly")]
    public class CustomerController : Controller
    {
        private BangazonContext _context;
        public CustomerController(BangazonContext ctx)
        {
            _context = ctx;
        }

        // GET All Customers
        //http://localhost:5000/Customer/ - Returns ALL customers
        [HttpGet]
        public IActionResult Get(bool? active)
        {
            if(active is bool)
            {
                if(active == true)
                {
                    //Gets Customers who placed an order
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
                    //Gets Customers who have NOT placed an order
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
                IQueryable<object> customers = from customer in _context.Customer select customer;

                if (customers == null)
                {
                    return NotFound();
                }
                return Ok(customers);
            }
            else
            {
                return BadRequest();
            }
        }

        // GET Single Customer
        // http://localhost:5000/Customer/{id}
        [HttpGet("{id}", Name = "GetSingleCustomer")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Customer customer = _context.Customer.Single(m => m.CustomerId == id);

                if (customer == null)
                {
                    return NotFound();
                }

                return Ok(customer);
            }

            catch (System.InvalidOperationException ex)
            {
                return NotFound(ex);
            }

        }
        // POST
        // //http://localhost:5000/Customer/ Posts new customer
        [HttpPost]
        public IActionResult Post([FromBody] Customer newCustomer)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Customer.Add(newCustomer);

            try
            {
                _context.SaveChanges();
            }

            catch (DbUpdateException)
            {

                if (CustomerExists(newCustomer.CustomerId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }

                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetSingleCustomer", new { id = newCustomer.CustomerId }, newCustomer);
        }

        private bool CustomerExists(int customerId)
        {
          return _context.Customer.Count(e => e.CustomerId == customerId) > 0;
        }

        // PUT
         //http://localhost:5000/Customer/{id} - edits existing customer
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Customer modifiedCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != modifiedCustomer.CustomerId)
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

            return Ok (modifiedCustomer);
        }


    }
}
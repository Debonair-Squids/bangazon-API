using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bangazon_inc.Data;
using bangazon_inc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace bangazon_inc.Controllers
{
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        private BangazonContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public CustomerController(BangazonContext ctx)
        {
            _context = ctx;
        }
            [HttpGet]
        public IActionResult Get()
        {
            var customer = _context.Customer.ToList();
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        // GET
        [HttpGet("{id}", Name = "GetSingleCustomer")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Customer Customer = _context.Customer.Single(o => o.CustomerId == id);

                if (Customer == null)
                {
                    return NotFound();
                }

                return Ok(Customer);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound(ex);
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Customer Customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Customer.Add(Customer);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(Customer.CustomerId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("GetSingleCustomer", new { id = Customer.CustomerId }, Customer);
        }

        // PUT api/values/5
        [HttpPut("{Id}")]
        public IActionResult Put(int Id, [FromBody]Customer Customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Id != Customer.CustomerId)
            {
                return BadRequest();
            }
            _context.Customer.Update(Customer);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(Id))
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

        // DELETE
        // [HttpDelete("{id}")]
        // public IActionResult Delete(int id)
        // {
        //     Customer Customer = _context.Customer.Single(o => o.CustomerId == id);

        //     if (Customer == null)
        //     {
        //         return NotFound();
        //     }
        //     _context.Customer.Remove(Customer);
        //     _context.SaveChanges();
        //     return Ok(Customer);
        // }

        private bool CustomerExists(int CustomerId)
        {
            return _context.Customer.Any(o => o.CustomerId == CustomerId);
        }
    }
}


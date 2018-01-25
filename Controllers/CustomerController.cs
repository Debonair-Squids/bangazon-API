using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using bangazon_inc.Data;
using bangazon_inc.Models;

namespace bangazon_inc.Controllers
{
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

        // GET api/values/5
        [HttpGet("{id}", Name = "GetSingleCustomer")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Customer customer = _context.Customer.Single(g => g.CustomerId == id);

                if (customer == null)
                {
                    return NotFound();
                }

                return Ok(customer);
            }
            catch (System.InvalidOperationException)
            {
                return NotFound();
            }
        }
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Customer.Add(customer);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.CustomerId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("GetSingleCustomer", new { id = customer.CustomerId }, customer);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerId)
            {
                return BadRequest();
            }
            _context.Customer.Update(customer);
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

        private bool CustomerExists(int customerId)
        {
            return _context.Customer.Any(g => g.CustomerId == customerId);
        }
    }

    internal class StatusCodes
    {
        public static int Status409Conflict { get; internal set; }
        public static int Status204NoContent { get; internal set; }
    }
}


    
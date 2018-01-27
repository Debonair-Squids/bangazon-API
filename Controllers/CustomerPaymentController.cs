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
    //sets the route to the name of the website/'CustomerPayment'
    [Route("[controller]")]


    //PUT/POST/GET/ CustomerPayment
    public class CustomerPaymentController : Controller
    {
        //Sets up an empty variable _context that will  be a reference of our BangazonContext class
        private BangazonContext _context;
        public CustomerPaymentController(BangazonContext ctx)
        {
            _context = ctx;
        }

        // GET all CustomerPayments http://localhost:5000/CustomerPayment
        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> customerPayments = from customerPayment in _context.CustomerPayment select customerPayment;

            if (customerPayments == null)
            {
                return NotFound();
            }

            return Ok(customerPayments);
        }
        // Check if the CustomerPayment exists in the database
        private bool CustomerPaymentExists(int CustomerPaymentId)
        {
            return _context.CustomerPayment.Count(e => e.CustomerPaymentId == CustomerPaymentId) > 0;
        }
        // GET Single CustomerPayment http://localhost:5000/CustomerPayment/1

        [HttpGet("{id}", Name = "GetSingleCustomerPayment")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                CustomerPayment SingleCustomerPayment = _context.CustomerPayment.Single(m => m.CustomerPaymentId == id);

                if (SingleCustomerPayment == null)
                {
                    return NotFound();
                }

                return Ok(SingleCustomerPayment);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound(ex);
            }
        }

        // POST New CustomerPayment http://localhost:5000/CustomerPayment/
        // Object: {
        // "AccountNumber": 879988,
        // "PaymentTypeId": 3,
        // "CustomerId": 3
        // }
        [HttpPost]
        public IActionResult Post([FromBody] CustomerPayment newCustomerPayment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CustomerPayment.Add(newCustomerPayment);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CustomerPaymentExists(newCustomerPayment.CustomerPaymentId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetSingleCustomerPayment", new { id = newCustomerPayment.CustomerPaymentId }, newCustomerPayment);
        }


        // PUT Edit CustomerPayment http://localhost:5000/CustomerPayment/1

        // Object:
        // {
        // 	"customerPaymentId": 1,
        //  "accountNumber": 1222376,
        //  "paymentTypeId": 3,
        //  "customerId": 2
        // }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CustomerPayment editCustomerPayment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != editCustomerPayment.CustomerPaymentId)
            {
                return BadRequest();
            }

            _context.Entry(editCustomerPayment).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerPaymentExists(id))
                {
                    return new StatusCodeResult(StatusCodes.Status204NoContent);
                }
                else
                {
                    throw;
                }
            }


            return CreatedAtRoute("GetSingleCustomerPayment", new { id = editCustomerPayment.CustomerPaymentId }, editCustomerPayment);
        }
        // DELETE CustomerPayment http://localhost:5000/CustomerPayment/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CustomerPayment deleteCustomerPayment = _context.CustomerPayment.Single(m => m.CustomerPaymentId == id);
            if (deleteCustomerPayment == null)
            {
                return NotFound();
            }

            _context.CustomerPayment.Remove(deleteCustomerPayment);
            _context.SaveChanges();

            return Ok(deleteCustomerPayment);
        }


    }
}
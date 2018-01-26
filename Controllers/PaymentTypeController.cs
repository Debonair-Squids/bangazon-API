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


     // Class to PUT/POST/GET/DELETE payment type
    public class PaymentTypeController : Controller
    {
        //Sets up an empty variable _context that will  be a reference of our BangazonAPIContext class
        private BangazonContext _context;
        public PaymentTypeController(BangazonContext ctx)
        {
            _context = ctx;
        }

        // Get All payment type
        // ex. url/paymenttype
        // returns all payment types if any exist

        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> paymentTypes = from paymentType in _context.PaymentType select paymentType;

            if (paymentTypes == null)
            {
                return NotFound();
            }

            return Ok(paymentTypes);

        }

        // // Get a single payment type by PaymentTypeId
        // // ex. paymenttype/4 etc.
        // [HttpGet("{id}", Name = "GetSinglePaymentType")]
        // public IActionResult Get([FromRoute] int id)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     try
        //     {
        //         PaymentType paymentType = _context.PaymentType.Single(m => m.PaymentTypeId == id);

        //         if (paymentType == null)
        //         {
        //             return NotFound();
        //         }

        //         return Ok(paymentType);
        //     }
        //     catch (System.InvalidOperationException ex)
        //     {
        //         return NotFound(ex);
        //     }
        // }

        // Post a new payment type
        // ex. /paymenttype
        // Requires an Object:
        // {
        //     "Name": "Example",
        //     "AccountNumber": ex. 1123,
        //     "CustomerID": ex. 1
        // }

        [HttpPost]
        public IActionResult Post([FromBody] PaymentType newPaymentType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PaymentType.Add(newPaymentType);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PaymentTypeExists(newPaymentType.PaymentTypeId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetSinglePaymentType", new { id = newPaymentType.PaymentTypeId }, newPaymentType);
        }
        // Helper method to check if the payment type exists in the database
        private bool PaymentTypeExists(int PaymentTypeId)
        {
          return _context.PaymentType.Count(e => e.PaymentTypeId == PaymentTypeId) > 0;
        }

        // Edit a payment type
        // ex. /paymenttype/4
        // Requires an Object:
        // {
        //     "PaymentTypeId": ex. 1,
        //     "AccountNumber": ex. 2,
        //     "CustomerID": ex. 3,
        //     "Name": "Example"
        // }
        // [HttpPut("{id}")]
        // public IActionResult Put(int id, [FromBody] PaymentType modifiedPaymentType)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     if (id != modifiedPaymentType.PaymentTypeId)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(modifiedPaymentType).State = EntityState.Modified;

        //     try
        //     {
        //         _context.SaveChanges();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!PaymentTypeExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return new StatusCodeResult(StatusCodes.Status204NoContent);
        // }

        // // Deletes a payment type
        // // ex. paymenttype/4
        // [HttpDelete("{id}")]
        // public IActionResult Delete(int id)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     PaymentType paymentType = _context.PaymentType.Single(m => m.PaymentTypeId == id);
        //     if (paymentType == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.PaymentType.Remove(paymentType);
        //     _context.SaveChanges();

        //     return Ok(paymentType);
        // }
    }
}

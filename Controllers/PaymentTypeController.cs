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
    //sets the route to the name of the website/'Department'
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

        // Get All PaymentType PaymentType http://localhost:5000/PaymentType/9


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

        // Get Single PaymentType PaymentType http://localhost:5000/PaymentType/1
        [HttpGet("{id}", Name = "GetSinglePaymentType")]
        public IActionResult Get([FromRoute] int id)
        {
            //if you request anything other than an Id you will get a return of BadRequest.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //will search the _context.PaymentType for an entry that has the id we are looking for
                //if found, will return that PaymentType
                //if not found will return 404.
                PaymentType singlePaymentType = _context.PaymentType.Single(m => m.PaymentTypeId == id);

                if (singlePaymentType == null)
                {
                    return NotFound();
                }

                return Ok(singlePaymentType);
            }
            //if the try statement fails for some reason, will return error of what happened.
            catch (System.InvalidOperationException ex)
            {
                return NotFound(ex);
            }
        }
        // Helper method to check if the payment type exists in the database
        private bool PaymentTypeExists(int PaymentTypeId)
        {
            return _context.PaymentType.Count(e => e.PaymentTypeId == PaymentTypeId) > 0;
        }
        // PaymentType http://localhost:5000/PaymentType/
        // Object:
        // {
        //     "PaymentTypeName": "Visa"
        // }

        [HttpPost]
        public IActionResult Post([FromBody]PaymentType newPaymentType)
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



        // Edit PaymentType http://localhost:5000/PaymentType/9
        // Object:
        // {
        //     "PaymentTypeId": 9,
        //     "PaymentTypeName": "Master"
        // }
        // Can not change PaymentTypeId
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]PaymentType editPaymentType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != editPaymentType.PaymentTypeId)
            {
                return BadRequest();
            }

            _context.Entry(editPaymentType).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentTypeExists(id))
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

        // Deletes PaymentType http://localhost:5000/PaymentType/9

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PaymentType deletePaymentType = _context.PaymentType.Single(m => m.PaymentTypeId == id);
            if (deletePaymentType == null)
            {
                return NotFound();
            }
            _context.PaymentType.Remove(deletePaymentType);
            _context.SaveChanges();

            return Ok(deletePaymentType);
        }
    }
}

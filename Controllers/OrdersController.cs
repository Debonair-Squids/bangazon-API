using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bangazon_inc.Data;
using bangazon_inc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bangazon_inc.Controllers
{
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        private BangazonContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public OrdersController(BangazonContext ctx)
        {
            _context = ctx;
        }
            [HttpGet]
        public IActionResult Get()
        {
            var orders = _context.Orders.ToList();
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
        }

        // GET
        [HttpGet("{id}", Name = "GetSingleOrder")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Orders Orders = _context.Orders.Single(o => o.OrderId == id);

                if (Orders == null)
                {
                    return NotFound();
                }

                return Ok(Orders);
            }
            catch (System.InvalidOperationException crex)
            {
                return NotFound();
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Orders Orders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Orders.Add(Orders);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OrderExists(Orders.OrderId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("GetSingleOrder", new { id = Orders.OrderId }, Orders);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Orders Orders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Orders.OrderId)
            {
                return BadRequest();
            }
            _context.Orders.Update(Orders);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Orders Orders = _context.Orders.Single(o => o.OrderId == id);

            if (Orders == null)
            {
                return NotFound();
            }
            _context.Orders.Remove(Orders);
            _context.SaveChanges();
            return Ok(Orders);
        }

        private bool OrderExists(int OrderId)
        {
            return _context.Orders.Any(o => o.OrderId == OrderId);
        }
    }
}
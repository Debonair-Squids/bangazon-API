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
    //sets the route to the name of the website/'Computer'
    [Route("[controller]")]


    //PUT/POST/GET/ Computer
    public class ComputerController : Controller
    {
        //Sets up an empty variable _context that will  be a reference of our BangazonAPIContext class
        private BangazonContext _context;
        public ComputerController(BangazonContext ctx)
        {
            _context = ctx;
        }

        // GET all Computers http://localhost:5000/Computer
        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> computers = from computer in _context.Computer select computer;

            if (computers == null)
            {
                return NotFound();
            }

            return Ok(computers);
        }

        // GET Single Computer http://localhost:5000/Computer/9

        [HttpGet("{id}", Name = "GetSingleComputer")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Computer SingleComputer = _context.Computer.Single(m => m.ComputerId == id);

                if (SingleComputer == null)
                {
                    return NotFound();
                }

                return Ok(SingleComputer);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound(ex);
            }
        }

        // POST New Computer http://localhost:5000/Computer/
        // Object: {"DatePurchased": "mm-dd-yyyy", "dateDecommissioned": "na","activeStatus": true}

        [HttpPost]
        public IActionResult Post([FromBody] Computer newComputer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Computer.Add(newComputer);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ComputerExists(newComputer.ComputerId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetSingleComputer", new { id = newComputer.ComputerId }, newComputer);
        }
        // Checks if the computer already exists in the database
        private bool ComputerExists(int ComputerId)
        {
          return _context.Computer.Count(e => e.ComputerId == ComputerId) > 0;
        }

        // // PUT url/Computer/{id}
        // // Edits a Specific Computer from the database
        // // Requires an Object:
        // // {
        // //     "ComputerId": 1,
        // //     "datePurchased": "1988-01-18T00:00:00",
        // //     "dateDecomissioned": "0001-01-01T00:00:00"
        // // }

        // [HttpPut("{id}")]
        // public IActionResult Put(int id, [FromBody] Computer modifiedComputer)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     if (id != modifiedComputer.ComputerId)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(modifiedComputer).State = EntityState.Modified;

        //     try
        //     {
        //         _context.SaveChanges();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!ComputerExists(id))
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
        // // DELETE url/Computer/{id}
        // // Removes a specific Computer from the database correlated to the id in the url
        // [HttpDelete("{id}")]
        // public IActionResult Delete(int id)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     Computer computer = _context.Computer.Single(m => m.ComputerId == id);
        //     if (computer == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.Computer.Remove(computer);
        //     _context.SaveChanges();

        //     return Ok(computer);
        // }

    }
}
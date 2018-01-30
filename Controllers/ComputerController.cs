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
    [EnableCors("BangazonAllowed")]


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
            // No computers were found, send response with 404 status code
            if (computers == null)
            {
                return NotFound();
            }
            // Send response with 200 status code and newly created Computer in body
            return Ok(computers);
        }
        // Check if the computer exists in the database
        private bool ComputerExists(int ComputerId)
        {
            //  Returns a True or false if the ComputerId match count is greater than 0.
            return _context.Computer.Count(e => e.ComputerId == ComputerId) > 0;
        }
        // GET Single Computer http://localhost:5000/Computer/9

        [HttpGet("{id}", Name = "GetSingleComputer")]
        public IActionResult Get([FromRoute] int id)
        {
            /*
                This condition validates the values in model binding.
                In this case, it validates that the id value is an integer.
                If the following URL is requested, model validation will
                fail - because the string of 'chicken' is not an integer -
                and the client will receive a message to that effect.
                    GET http://localhost:5000/api/Computer/chicken
             */
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
            /*
                Model validation works differently here, since there
                is a complex type being detected with ([FromBody] Computer computer).
                This method will extract the key/value pairs from the JSON
                object that is posted, and create a new instance of the Computer
                model class, with the corresponding properties set.
                If any of the validations fail, such as length of string values,
                if a value is required, etc., then the API will respond that
                it is a bad request.
             */
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Add the computer to the database context
            _context.Computer.Add(newComputer);

            try
            {
                // Commit the newly created computer to the database
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
            // The CreatedAtRoute method will return the newly created computer in the body of the response
            return CreatedAtRoute("GetSingleComputer", new { id = newComputer.ComputerId }, newComputer);
        }


        // PUT Edit Computer http://localhost:5000/Computer/1
        // PUT requires you to send the entire object in the request, not just the field that you want to change.

        // Object:
        // {
        //     "ComputerId": 1,
        //     "datePurchased": "1-1-2001",
        //     "dateDecomissioned": "1-1-2001",
        //     "activeStatus": True
        // }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Computer editComputer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != editComputer.ComputerId)
            {
                return BadRequest();
            }

            _context.Entry(editComputer).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComputerExists(id))
                {
                    return new StatusCodeResult(StatusCodes.Status204NoContent);
                }
                else
                {
                    throw;
                }
            }

            // The CreatedAtRoute method will return the newly created computer in the body of the response

            return CreatedAtRoute("GetSingleComputer", new { id = editComputer.ComputerId }, editComputer);
        }
        // DELETE Computer http://localhost:5000/Computer/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Computer deleteComputer = _context.Computer.Single(m => m.ComputerId == id);
            if (deleteComputer == null)
            {
                return NotFound();
            }

            _context.Computer.Remove(deleteComputer);
            _context.SaveChanges();
            // Send response with 200 status code and newly created computer in body
            return Ok(deleteComputer);
        }


    }
}
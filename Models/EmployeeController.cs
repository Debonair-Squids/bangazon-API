using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using bangazon_inc.Models;
using bangazon_inc.Data;

namespace bangazon_inc.Controllers
{
    //Author Paul Ellis
    //sets the route to the name of the website/'Employee'
    [Route("[controller]")]

    //creates a new EmployeeController class
    public class EmployeeController : Controller
    {
        //variable to hold the context instance
        private BangazonContext _context;
        //create instance of the context
        public EmployeeController(BangazonContext ctx)
        {
            _context = ctx;
        }

        
        //returns a boolean of whether the employee exist.  Will be called in methods below
        private bool EmployeeExists(int employeeId)
        {
            return _context.Employee.Count(e => e.EmployeeId == employeeId) > 0;
        }

        // General Get Method
        //<designated address>/Employee/ will return a list of all Employees. 
        [HttpGet]

        public IActionResult Get()
        {
            //Sets a new IQuerable Collection of <objects> that will be filled with each instance of _context.Employee
            IQueryable<Employee> Employees = from Employee in _context.Employee select Employee;

            //if the collection is empty will return NotFound and exit the method. 
            if (Employees == null)
            {
                return NotFound();
            }

            //otherwise return list of the Employees
            return Ok(Employees);

        }

        // GET Single Employee
         //http://localhost:5000/Employee/{id} will return info on a single Employee based on Employee.Id
        [HttpGet("{id}", Name = "GetSingleEmployee")]

        //will run Get based on the id from the url route. 
        public IActionResult Get([FromRoute] int id)
        {
            //if you request anything other than an Id you will get a return of BadRequest. 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //searches the _context.Employee for an entry that has the matching EmployeeId value
                Employee singleEmployee = _context.Employee.Single(m => m.EmployeeId == id);

                if (singleEmployee == null)
                {
                    return NotFound();
                }
                
                return Ok(singleEmployee);
            }
            //if it fails, send the error back
            catch (System.InvalidOperationException ex)
            {
                return NotFound(ex);
            }
        }

           // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Employee e)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Employee.Add(e);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EmployeeExists(e.EmployeeId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("GetSingleEmployee", new { id = e.EmployeeId }, e);
        }

        // PUT method to change values in a table
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Employee e)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != e.EmployeeId)
            {
                return BadRequest();
            }
            _context.Employee.Update(e);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

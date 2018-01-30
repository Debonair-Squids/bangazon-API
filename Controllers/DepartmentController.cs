using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using bangazon_inc.Models;
using bangazon_inc.Data;
using Microsoft.AspNetCore.Cors;

namespace bangazon_inc.Controllers
{
    //Author Paul Ellis
    //sets the route to the name of the website/'Department'
    [Route("[controller]")]
    [EnableCors("BangazonAllowed")]

    //creates a new DepartmentController class
    public class DepartmentController : Controller
    {
        //variable to hold the context instance
        private BangazonContext _context;
        //create instance of the context
        public DepartmentController(BangazonContext ctx)
        {
            _context = ctx;
        }


        //returns a boolean of whether the department exist.  Will be called in methods below
        private bool DepartmentExists(int departmentId)
        {
            return _context.Department.Count(e => e.DepartmentId == departmentId) > 0;
        }

        // General Get Method
        //<designated address>/department/ will return a list of all Departments. 

        [HttpGet]

        public IActionResult Get()
        {
            //Sets a new IQuerable Collection of <objects> that will be filled with each instance of _context.Department
            IQueryable<Department> Departments = from department in _context.Department select department;

            //if the collection is empty will retur NotFound and exit the method. 
            if (Departments == null)
            {
                return NotFound();
            }

            //otherwise return list of the Departments
            return Ok(Departments);

        }

        // GET Single Department
        //http://localhost:5000/Department/{id} will return info on a single Department based on Department.Id
        [HttpGet("{id}", Name = "GetSingleDepartment")]

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
                //searches the _context.Department for an entry that has the matching DepartmentId value
                Department singleDepartment = _context.Department.Single(m => m.DepartmentId == id);

                if (singleDepartment == null)
                {
                    return NotFound();
                }

                return Ok(singleDepartment);
            }
            //if it fails, send the error back
            catch (System.InvalidOperationException ex)
            {
                return NotFound(ex);
            }
        }

        // POST api/values
        //Exampe raw json object for testing:
        /*
        {
        	"Budget": 5000.0,
	        "Name": "TestName"
        } 
        */
        [HttpPost]
        public IActionResult Post([FromBody]Department dept)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Department.Add(dept);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DepartmentExists(dept.DepartmentId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("GetSingleDepartment", new { id = dept.DepartmentId }, dept);
        }

        // PUT method to change values in a table
        //http://localhost:5000/Department/1
        //Example raw json object for testing
        /*
        {
            "DepartmentId": 1,
            "Budget": 2000.0,
            "Name": "TestName"
        }
         */
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Department dept)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dept.DepartmentId)
            {
                return BadRequest();
            }
            _context.Department.Update(dept);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
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

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
    //sets the route to the name of the website/'Department'
    [Route("[controller]")]

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

        private bool DepartmentExists(int departmentID)
        {
            return _context.Department.Count(e => e.DepartmentId == departmentID) > 0;
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
        [HttpPost]
        public IActionResult Post([FromBody]Department dept)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(dept).State = EntityState.Modified;

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


    }
}

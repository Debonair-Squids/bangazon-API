using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using bangazon_inc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace bangazon_inc.Controllers
{
    //Author Paul Ellis
    //sets the route to the name of the website/'department'
    [Route("api/[controller]")]

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

        // GET api/values
        //<designated website>/department/  will return a list of all Departments. 

            // [HttpGet]
            // public IEnumerable<string> Get()
            // {
            //     return new string[] { "value1", "value2" };
            // }

            // // GET api/values/5
            // [HttpGet("{id}")]
            // public string Get(int id)
            // {
            //     return "value";
            // }

            // POST api/values
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
            return CreatedAtRoute("GetSingleAlbum", new { id = dept.DepartmentId }, dept);
        }

        // // PUT api/values/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody]string value)
        // {
        // }

    }
}

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
    public class CustomerController : Controller
    {
        private BangazonContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public CustomerController(BangazonContext ctx)
        {
            _context = ctx;
        }

        private bool CustomerExists(int customerID)
        {
            return _context.Customer.Count(e => e.CustomerId == customerID) > 0;
        }

        // GET Single Department
         //http://localhost:5000/Department/{id} will return info on a single Department based on ID 
        [HttpGet("{id}", Name = "GetSingleCustomer")]

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
                //will search the _context.Department for an entry that has the id we are looking for
                //if found, will return that Department
                //if not found will return 404. 
                Customer singleCustomer = _context.Customer.Single(m => m.CustomerId == id);

                if (singleCustomer == null)
                {
                    return NotFound();
                }
                
                return Ok(singleCustomer);
            }
            //if the try statement fails for some reason, will return error of what happened. 
            catch (System.InvalidOperationException ex)
            {
                return NotFound(ex);
            }
        }

           // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Customer.Add(customer);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.CustomerId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("GetSingleCustomer", new { id = customer.CustomerId }, customer);
        }

        // // PUT api/values/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody]string value)
        // {
        // }

    }
}





// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Rendering;
// using Microsoft.EntityFrameworkCore;
// using bangazon_inc.Data;
// using bangazon_inc.Models;

// namespace bangazon_inc.Controllers
// {
//     [Route("[controller]")]
//     public class CustomerController : Controller
//     {
//         private BangazonContext _context;    
//         // Constructor method to create an instance of context to communicate with our database.
//         public CustomerController(BangazonContext ctx)
//         {
//             _context = ctx;
//         }

//         [HttpGet]
//         public IActionResult Get()
//         {
//             var customer = _context.Customer.ToList();
//             if (customer == null)
//             {
//                 return NotFound();
//             }
//             return Ok(customer);
//         }

//         // GET api/values/5
//         [HttpGet("{id}", Name = "GetSingleCustomer")]
//         public IActionResult Get(int id)
//         {
//             if (!ModelState.IsValid)
//             {
//                 return BadRequest(ModelState);
//             }

//             try
//             {
//                 Customer customer = _context.Customer.Single(g => g.CustomerId == id);

//                 if (customer == null)
//                 {
//                     return NotFound();
//                 }

//                 return Ok(customer);
//             }
//             catch (System.InvalidOperationException)
//             {
//                 return NotFound();
//             }
//         }
//         // POST api/values
//         [HttpPost]
//         public IActionResult Post([FromBody]Customer customer)
//         {
//             if (!ModelState.IsValid)
//             {
//                 return BadRequest(ModelState);
//             }

//             _context.Customer.Add(customer);

//             try
//             {
//                 _context.SaveChanges();
//             }
//             catch (DbUpdateException)
//             {
//                 if (CustomerExists(customer.CustomerId))
//                 {
//                     return new StatusCodeResult(StatusCodes.Status409Conflict);
//                 }
//                 else
//                 {
//                     throw;
//                 }
//             }
//             return CreatedAtRoute("GetSingleCustomer", new { id = customer.CustomerId }, customer);
//         }

//         // PUT api/values/5
//         [HttpPut("{id}")]
//         public IActionResult Put(int id, [FromBody]Customer customer)
//         {
//             if (!ModelState.IsValid)
//             {
//                 return BadRequest(ModelState);
//             }

//             if (id != customer.CustomerId)
//             {
//                 return BadRequest();
//             }
//             _context.Customer.Update(customer);
//             try
//             {
//                 _context.SaveChanges();
//             }
//             catch (DbUpdateConcurrencyException)
//             {
//                 if (!CustomerExists(id))
//                 {
//                     return NotFound();
//                 }
//                 else
//                 {
//                     throw;
//                 }
//             }

//             return new StatusCodeResult(StatusCodes.Status204NoContent);
//         }

//         private bool CustomerExists(int customerId)
//         {
//             return _context.Customer.Any(g => g.CustomerId == customerId);
//         }
//     }

//     internal class StatusCodes
//     {
//         public static int Status409Conflict { get; internal set; }
//         public static int Status204NoContent { get; internal set; }
//     }
// }



// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using BangazonAPI.Data;
// using Microsoft.AspNetCore.Mvc;

// namespace bangazon_inc.Controllers
// {
//     [Route("api/[controller]")]
//     public class OrderController : Controller
//     {
//         private BangazonContext _context;
//         // Constructor method to create an instance of context to communicate with our database.
//         public OrderController(BangazonContext ctx)
//         {
//             _context = ctx;
//         }

//         [HttpGet]
//         public IActionResult Get()
//         {
//             var orders = _context.Orders.ToList();
//             if (orders == null)
//             {
//                 return NotFound();
//             }
//             return Ok(orders);
//         }

//         // GET api/album/5
//         [HttpGet("{id}", Name = "GetSingleOrder")]
//         public IActionResult Get(int OrderId)
//         {
//             if (!ModelState.IsValid)
//             {
//                 return BadRequest(ModelState);
//             }

//             try
//             {
//                 Orders order = _context.Orders.Single(g => g.OrderId == id);

//                 if (order == null)
//                 {
//                     return NotFound();
//                 }

//                 return Ok(order);
//             }
//             catch (System.InvalidOperationException ex)
//             {
//                 return NotFound();
//             }
//         }

//         // POST api/Order
//         [HttpPost]
//         public void Post([FromBody]string value)
//         {
//         }

//         // PUT api/Order/5
//         [HttpPut("{id}")]
//         public void Put(int id, [FromBody]string value)
//         {
//         }

//         // DELETE api/values/5
//         [HttpDelete("{id}")]
//         public void Delete(int id)
//         {
//         }
//     }
// }

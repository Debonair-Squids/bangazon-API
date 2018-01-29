using System.Linq;
using Microsoft.AspNetCore.Mvc;
using bangazon_inc.Data;
using bangazon_inc.Models;
using Microsoft.EntityFrameworkCore;

namespace bangazon_inc.Controllers
{
    [Route("[controller]")]

    //Create a new class for the product type table controller
    public class ProductController : Controller
    {
        //Declare an empty variable "_context" to store BangazonContext Class
        private BangazonContext _context;

        //Create a constructor that is equal to BangazonContext. Multiple controller actions will require the same service.  Create a constructor to request those dependencies.
        public ProductController(BangazonContext ctx){
            _context = ctx;
        }


        //Get all product types
        [HttpGet]
        public IActionResult Get()
        {
            //
            var Product = _context.Product.ToList();
            if (Product == null){
                return NotFound();
            }
            return Ok(Product);
        }

        // GET a single product type
        [HttpGet("{id}", Name = "GetSingleProduct")]
    
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Product Product = _context.Product.Single(g => g.ProductId == id);

                if(Product == null)
                {
                    return NotFound();
                }
                return Ok(Product);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound(ex);
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Product newProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Product.Add(newProduct);
            
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(newProduct.ProductId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetSingleProduct", new { id = newProduct.ProductId }, newProduct);
        }

        private bool ProductExists(int ProductId)
        {
            return _context.Product.Any(g => g.ProductId == ProductId);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
              public IActionResult PUT (int id, [FromBody] Product newProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if( id != newProduct.ProductId)
            {
                return BadRequest();
            }

            _context.Product.Update(newProduct);
            
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // DELETE api/values/5
        [HttpDelete("{id}")]
              public IActionResult DELETE (int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product product = _context.Product.Single(m => m.ProductId == id);

            if( product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            _context.SaveChanges();

            return Ok (product);

        }
    }
}
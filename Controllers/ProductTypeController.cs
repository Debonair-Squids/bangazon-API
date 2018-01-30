using System.Linq;
using Microsoft.AspNetCore.Mvc;
using bangazon_inc.Data;
using bangazon_inc.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;

namespace bangazon_inc.Controllers
{
    [Route("[controller]")]
    [EnableCors("BangazonAllowed")]

    //Create a new class for the product type table controller
    public class ProductTypeController : Controller
    {
        //Declare an empty variable "_context" to store BangazonContext Class
        private BangazonContext _context;

        //Create a constructor that is equal to BangazonContext. Multiple controller actions will require the same service.  Create a constructor to request those dependencies.
        public ProductTypeController(BangazonContext ctx){
            _context = ctx;
        }


        //Get all product types
        [HttpGet]
        public IActionResult Get()
        {
            //
            var ProductType = _context.ProductType.ToList();
            if (ProductType == null){
                return NotFound();
            }
            return Ok(ProductType);
        }

        // GET a single product type
        [HttpGet("{id}", Name = "GetSingleProductType")]
    
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProductType productType = _context.ProductType.Single(g => g.CategoryId == id);

                if(productType == null)
                {
                    return NotFound();
                }
                return Ok(productType);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound(ex);
            }
        }

        /* Sample POST request:
            {
                "CategoryName":
            }
        */
        [HttpPost]
        public IActionResult Post([FromBody] ProductType newProductType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ProductType.Add(newProductType);
            
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductTypeExists(newProductType.CategoryId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetSingleProductType", new { id = newProductType.CategoryId }, newProductType);
        }

        private bool ProductTypeExists(int CategoryId)
        {
            return _context.ProductType.Any(g => g.CategoryId == CategoryId);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
              public IActionResult PUT (int id, [FromBody] ProductType newProductType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if( id != newProductType.CategoryId)
            {
                return BadRequest();
            }

            _context.ProductType.Update(newProductType);
            
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductTypeExists(id))
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

            ProductType productType = _context.ProductType.Single(m => m.CategoryId == id);

            if( productType == null)
            {
                return NotFound();
            }

            _context.ProductType.Remove(productType);
            _context.SaveChanges();

            return Ok (productType);

        }
    }
}
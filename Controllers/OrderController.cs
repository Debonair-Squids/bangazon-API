using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace bangazon_inc.Controllers
{
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private BangazonContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public OrderController(BangazonContext ctx)
        {
            _context = ctx;
        }

        // GET order list
        [HttpGet("{id}", Name = "GetSingleOrder")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Orders Orders = _context.Order.Include("OrderProduct.Product").Single(m => m.OrderID == id);
                List <Product> theseProducts = new List <Product>();
                foreach (OrderProduct OrderProduct in order.OrderProduct)
                {
                    theseProducts.Add(OrderProduct.Product);
                }
                List <ProductOnOrderJSON> jSONProducts = new List <ProductOnOrderJSON>();
                foreach (Product product in theseProducts) {
                    int index = jSONProducts.FindIndex(x => x.ProductID == product.ProductID);
                    if (index != -1)
                    {
                        jSONProducts[index].Quantity ++;
                    }
                    else
                    {
                        ProductOnOrderJSON newProduct = new ProductOnOrderJSON()
                        {
                            ProductID = Product.ProductID,
                            Name = Product.Title,
                            Price = Product.Price,
                            Description = Product.Description,
                            Quantity = 1
                        };
                        jSONProducts.Add(newProduct);
                    }
                }
                OrderWithProductJSON orderWithProducts = new OrderWithProductJSON()
                {
                    OrderID = order.OrderID,
                    CustomerID = order.CustomerID,
                    PaymentTypeID = order.PaymentTypeID,
                    Products = jSONProducts
                };
                if (order == null)
                {
                    return NotFound();
                }

                return Ok(orderWithProducts);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound(ex);
            }
        }

        // POST
          [HttpPost]
        public IActionResult Post([FromBody] Orders newOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Order.Add(newOrder);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OrderExists(newOrder.OrderID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("GetSingleOrder", new { id = newOrder.OrderID }, newOrder);
        }


        // PUT
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Orders modifiedOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != modifiedOrder.OrderID)
            {
                return BadRequest();
            }

            _context.Entry(modifiedOrder).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Orders singleOrder = _context.Orders.Single(m => m.OrderID == id);
            if (singleOrder == null)
            {
                return NotFound();
            }
            _context.Orders.Remove(singleOrder);
            _context.SaveChanges();
            return Ok(singleOrder);
        }
    }
}
// // Adds a product to an order and creates a ProductOrder. This allows us to add multiple products to the same order.
        // // You need to send up a product order object.
        // // {"OrderID": integer, "ProductID": integer}
        // [HttpPost("addproduct")]
        // public IActionResult Post([FromBody] ProductOrder newProductOrder )
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }
        //     _context.ProductOrder.Add(newProductOrder);
        //     try
        //     {
        //         _context.SaveChanges();
        //     }
        //     catch (DbUpdateException)
        //     {
        //         if (OrderExists(newProductOrder.ProductOrderID))
        //         {
        //             return new StatusCodeResult(StatusCodes.Status409Conflict);
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }
        //     return Ok(newProductOrder);
        // }
        // // Helper method to check if the order already exists in the database
        // private bool OrderExists(int orderID)
        // {
        //   return _context.Order.Count(e => e.OrderID == orderID) > 0;
        // }

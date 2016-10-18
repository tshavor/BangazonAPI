using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bangazon.Data;
using Microsoft.EntityFrameworkCore;
using Bangazon.Models;
using Microsoft.AspNetCore.Http;

namespace Bangazon_API.Controllers
{

    [Route("[controller]")]
    public class ProductsController: Controller
    {
        private BangazonContext context;

        public ProductsController(BangazonContext ctx)
        {
            context = ctx;
        }

        // GET api/values
        [HttpGet]

        public IActionResult Get()
        {
            //in English, this means to select EVERYTHING from the customer table//(revised for products)
            IQueryable<object> products = from product in context.Product select product;

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);

        }

        [HttpGet("{id}", Name = "Getproducts")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Product product = context.Product.Single(m => m.ProductId == id);

                if (product == null)
                {
                    return NotFound();
                }
                
                return Ok(product);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }

        }
        
        // POST api/values
        [HttpPost]
public IActionResult Post([FromBody] Product product)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
//adding to a customer collection- not saving it yet!//
            context.Product.Add(product);
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(product.ProductId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetProduct", new { id = product.ProductId }, product);
        }
        // PUT api/values/5//////////////////////////////////////////////////////////////
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product product)
        {
            if (product == null || product.ProductId != id || !ModelState.IsValid)
            
            {
                return BadRequest(ModelState);
            }
            else
            {
                context.Entry(product).State= EntityState.Modified;
                context.SaveChanges();
            }

                return Ok(product);
        }


        // DELETE api/values/5/////////////////////////////////////////////////////////
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //this is performing a for-each loop on Orderid that's equal to that id thats ben deleted..
            Product product = context.Product.Single(m => m.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            context.Product.Remove(product);
            context.SaveChanges();

            return Ok(product);
        }
        private bool ProductExists(int id)
        {
            return context.Product.Count(e => e.ProductId == id) > 0;
        }

    }
}
   

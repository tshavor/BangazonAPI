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
    public class OrdersController: Controller
    {
        private BangazonContext context;

        public OrdersController(BangazonContext ctx)
        {
            context = ctx;
        }

        // GET api/values
        [HttpGet]
        
         public IActionResult Get()
        {
            //in English, this means to select EVERYTHING from the customer table//(modified)
            IQueryable<object> orders = from order in context.Order select order;

            if (orders == null)
            {
                return NotFound();
            }

            return Ok(orders);

        }

[HttpGet("{id}", Name = "GetOrder")]
        //int OrderId instead of int id?//
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Order order = context.Order.Single(m => m.OrderId == id);

                if (order == null)
                {
                    return NotFound();
                }
                
                return Ok(order);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }

        }
        //replaced per Steve with above code://
        // // GET api/values/5
        // [HttpGet("{id}")]
        // public string Get(int id)
        // {
        //     return "value";
        // }


        // POST api/values
        [HttpPost]
public IActionResult Post([FromBody] Order order)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
//adding to a customer collection- not saving it yet!//
            context.Order.Add(order);
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OrderExists(order.OrderId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetCustomer", new { id = order.OrderId }, order);
        }

        private bool OrderExists(int id)
        {
            return context.Order.Count(e => e.OrderId == id) > 0;
        }

        // PUT api/values/5///////////////////////////////////////////////////////
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Order order)
        {
            if (order == null || order.OrderId != id || !ModelState.IsValid)
            
            {
                return BadRequest(ModelState);
            }
            else
            {
                context.Entry(order).State= EntityState.Modified;
                context.SaveChanges();
            }

                return Ok(order);
        }

        // DELETE api/values/5///////////////////////////////////////////////////
        [HttpDelete("{id}")]
        public void Delete(int id)
        {





        }
    }
}
   

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
    public class CustomersController: Controller
    {
        private BangazonContext context;

        public CustomersController(BangazonContext ctx)
        {
            context = ctx;
        }

        // GET api/values////////////////////////////////////////////////////////////////
        [HttpGet]
        
         public IActionResult Get()
        {
            //in English, this means to select EVERYTHING (all rows) from the customer table.  This is an example of LINC language, and it is the reverse of SQL//
            IQueryable<object> customers = from customer in context.Customer select customer;

            if (customers == null)
            {
                return NotFound();
            }

            return Ok(customers);

        }

[HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Customer customer = context.Customer.Single(m => m.CustomerId == id);

                if (customer == null)
                {
                    return NotFound();
                }
                
                return Ok(customer);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }

        }


        // POST api/values////////////////////////////////////////////////////////////////
        [HttpPost]
public IActionResult Post([FromBody] Customer customer)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
//adding to a customer collection- not saving it yet!//
            context.Customer.Add(customer);
            try
            {
                context.SaveChanges();
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

            return CreatedAtRoute("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        private bool CustomerExists(int id)
        {
            return context.Customer.Count(e => e.CustomerId == id) > 0;
        }

        // PUT customers/5 (this is to be written!)//////////////////////////////////////////////
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Customer customer)
        {
    //Explanation: (1) the customer can't be null, (2) the customer id in the PUT request must match the Id in the URL, 
    // and (3) the customer must be a valid customer.../// 
            if (customer == null || customer.CustomerId != id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
// now, I need to write something that replaces the information in the database with the information sent in the PUT request///
            else
            {
                //context is the database structure in memory (also caled db context)//
                context.Entry(customer).State= EntityState.Modified;
                //context.Customer.Update();//modifies only the changed files.
                context.SaveChanges();
            }
//last, I need to send some sort of success message so that the sender knows that their PUT request was successful..///
                return Ok(customer);
        }

        // DELETE api/values/5 (this is to be written!)/////////////////////////
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
           {

  
           }

//example of [Delete] from Microsoft.////////////
// [HttpDelete("{id}")]
// public IActionResult Delete(string id)
// {
//     var todo = TodoItems.Find(id);
//     if (todo == null)
//     {
//         return NotFound();
//     }

//     TodoItems.Remove(id);
//     return new NoContentResult();
// }



    }
}
   

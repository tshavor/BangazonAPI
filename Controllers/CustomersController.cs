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

        // GET api/values
        [HttpGet]
        
         public IActionResult Get()
        {
            //in English, this means to select EVERYTHING from the customer table//
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
        //replaced per Steve with above code://
        // // GET api/values/5
        // [HttpGet("{id}")]
        // public string Get(int id)
        // {
        //     return "value";
        // }


        // POST api/values
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

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
   

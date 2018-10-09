using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerApplication.Api.Models;
using CustomerApplication.Api.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerApplication.Api.Controllers
{
    [ApiController]
    public class CustomersController : Controller
    {
        // Constructor Injection - Dependency Injection
        private ICustomerService<Customer, long> _customerService;
        public CustomersController(ICustomerService<Customer, long> customerService)
        {
            _customerService = customerService;
        }

        // GET api/Customers/Get 
        [HttpGet]
        [Route("api/[controller]/Get")]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            // Gets all customers
            var customers = _customerService.GetAll();

            return Ok(customers);
        }

        // GET api/Customers/Get/2  
        [HttpGet("{id}")]
        [Route("api/[controller]/Get/{id}")]
        public ActionResult<Customer> Get(int id)
        {
            // Gets customer based on Id
            var customer = _customerService.Get(id);

            // Checks customer isn't Null
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // POST api/Customers/Create 
        [HttpPost]
        [Route("api/[controller]/Create")]
        public ActionResult Post([FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Adds new customer
            _customerService.Add(customer);
            return CreatedAtAction("Get", new { id = customer.Id }, customer);
        }

        // POST api/Customers/Edit  
        [HttpPut]
        [Route("api/[controller]/Edit")]
        public ActionResult Put([FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Gets customer data for editing
            _customerService.Update(customer.Id, customer);
            return CreatedAtAction("Get", new { id = customer.Id }, customer);
        }

        // DELETE api/Customers/Delete/2 
        [HttpDelete("{id}")]
        [Route("api/[controller]/Delete/{id}")]
        public ActionResult<long> Delete(int id)
        {
            // Gets customer based on Id
            var customer = _customerService.Get(id);

            // Checks customer isn't Null
            if (customer == null)
            {
                return NotFound();
            }

            // Deletes customer based on Id
            var customerId = _customerService.Delete(id);

            return Ok(customerId);
        }
    }
}

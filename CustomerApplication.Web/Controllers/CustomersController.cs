using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CustomerApplication.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CustomerApplication.Web.Controllers
{
    public class CustomersController : Controller
    {
        // Note-JsonConvert.SerializeObject & JsonConvert.DeserializeObject are used to serialize and deserialize the data.

        CustomerAPI _customerAPI = new CustomerAPI();

        /// <summary>
        /// Method gets all customers data from the DB. It's defined as asynchronous
        /// to return the view for the index and customer list values.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            List<CustomerDTO> dto = new List<CustomerDTO>();

            HttpClient client = _customerAPI.InitializeClient();

            HttpResponseMessage res = await client.GetAsync("api/customers/Get");

            //Checking the response is successful or not which is sent using HttpClient    
            if (res.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api     
                var result = res.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing into the Employee list    
                dto = JsonConvert.DeserializeObject<List<CustomerDTO>>(result);

            }
            //returning the employee list to view    
            return View(dto);
        }

        // GET Customers/Create
        /// <summary>
        /// Method simply returns it's corresponding View.
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST Customers/Create
        /// <summary>
        /// Method uses Post attribute to be invoked on Post requests. Method handles adding
        /// new data to the DB aslong as the modelstate is valid on the posts.
        /// </summary>
        /// <param name="customer"> Customer object</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FirstName,Surname,Email,Password")] CustomerDTO customer)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = _customerAPI.InitializeClient();

                var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
                HttpResponseMessage res = client.PostAsync("api/customers/Create", content).Result;
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(customer);
        }

        // GET Customers/Edit/1 
        /// <summary>
        /// Method use id for a identifier parameter. If id is null nothing is found while
        /// if its not null return the corresponding view data.
        /// </summary>
        /// <param name="id">Identifier Parameter</param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<CustomerDTO> dto = new List<CustomerDTO>();
            HttpClient client = _customerAPI.InitializeClient();
            HttpResponseMessage res = await client.GetAsync("api/customers/Get");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                dto = JsonConvert.DeserializeObject<List<CustomerDTO>>(result);
            }

            var customer = dto.SingleOrDefault(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST Customers/Edit/1 
        /// <summary>
        /// Method updates the corresponding data to the database using the id.
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <param name="customer">Customer object</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("Id, FirstName, Surname, Email, Password")] CustomerDTO customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                HttpClient client = _customerAPI.InitializeClient();

                var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
                HttpResponseMessage res = client.PutAsync("api/customers/Edit", content).Result;
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(customer);
        }

        // GET Customers/Delete/1 
        /// <summary>
        /// Method takes the id as the parameter. If the id is null, it returns nothing. 
        /// If it isn't null, returns the corresponding View data.
        /// </summary>
        /// <param name="id">Id Identifier</param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<CustomerDTO> dto = new List<CustomerDTO>();
            HttpClient client = _customerAPI.InitializeClient();
            HttpResponseMessage res = await client.GetAsync("api/customers/Get");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                dto = JsonConvert.DeserializeObject<List<CustomerDTO>>(result);
            }

            var customer = dto.SingleOrDefault(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST Students/Delete/5  
        /// <summary>
        ///  Method deletes the corresponding data to the database using the id.
        /// </summary>
        /// <param name="id">customer Idendifier</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            HttpClient client = _customerAPI.InitializeClient();
            HttpResponseMessage res = client.DeleteAsync($"api/customers/Delete/{id}").Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return NotFound();
        }
    }
}
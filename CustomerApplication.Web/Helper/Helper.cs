using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CustomerApplication.Web.Helper
{
    /// <summary>
    /// CustomerAPI - InitilizeClient method Initializes the HttpClient Object.
    /// This is needed to access the API endpoints in the Controller.
    /// </summary>
    public class CustomerAPI
    {          
        private string _apiBaseURI = "http://localhost:2202";
        public HttpClient InitializeClient()
        {
            var client = new HttpClient();
            // Passes base url service.    
            client.BaseAddress = new Uri(_apiBaseURI);

            client.DefaultRequestHeaders.Clear();
            // Defines data request format.   
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }

    /// <summary>
    /// DTO - Mimics Customers class from WebApi project.
    /// </summary>
    public class CustomerDTO
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

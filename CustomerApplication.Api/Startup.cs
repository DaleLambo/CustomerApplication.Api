using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerApplication.Api.Models;
using CustomerApplication.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CustomerApplication.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // SQL DATABASE EXAMPLE :-
            // My DB was setup on my Local, I've just exported the DB so it can be imported.
            // I've currently set it up using the in-memory Database in the meantime. I've added the relevant connectionStrings into appsettings.json file and configuration
            // to show you I know how to do it. Obviously you would need to run the migrations and update the Database in the package manager console e.g.
            // PM> add-migration CustomerApplication.Api.Models.ApplicationContext
            // PM> update-database
            //services.AddDbContext<ApplicationContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:CustomerApplicationDB"]));

            // Add framework services.
            services.AddDbContext<ApplicationContext>(opt => opt.UseInMemoryDatabase("CustomerList"));// Currently using In-memory database.

            //services.AddSingleton(typeof(ICustomerService<Customer, long>), typeof(CustomerService));
            services.AddScoped(typeof(ICustomerService<Customer, long>), typeof(CustomerService));
            //services.AddTransient<ICustomerService<Customer, long>, CustomerService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}

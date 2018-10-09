# CustomerApplication.Api
Example ASP.NET CORE 2.1 WebAPI project that can be used for making web requests and from the front end. This project contains Unit tests, Restful API and Web Application. The solution should be setup to run 
(Multiple Startup Projects) both the WebApi and UI when building.
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CustomerApplication.API Notes:

WebApi was built using .NET Core ASP.NET 2.1.
Since the Database was on my local machine, I've just exported the Database as a bacpac file so it can be imported. However in the meantime, I've currently set it up using the in-memory Database. I've added the 
relevant connectionStrings into the appsettings.json file and configuration to show you I know how to do it. Obviously you would need to run the migrations and update the Database in the package manager console.
The WebApi has different routes for CRUD operations endpoints:

GetAll - /api/customers/Get
GetById - /api/customers/Get/{Id}
Create - /api/customers/Create
Edit - /api/customers/Edit
Delete - /api/customers/Delete/{Id}

The unit tests are located in the CustomerApplication.Api-Test project in the solution. They are using Moq and Xunit.

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CustomerApplication.Web Notes: 

The user interface was built using MVC in the CustomerApplication.Web project in the Solution. The Helper folder/class contains an CustomerAPI class for initializing the HttpClient object to access the WebApi 
endpoints. The CustomerDTO class mimics the Customer class from the API for retrieve the data. In the CustomerController we use Newtonsoft's JsonConvert.SerializeObject and JsonConvert.DeserializeObject to 
serialize and deserialize the data from the API/WebApplication.Api and UI/WebAplication.Web. You will find the corresponding views Index, Create, Edit and Delete:

/customers
/customers/Create
/customers/Edit/1
/customers/Delete/1

The solution should be setup to run (Multiple Startup Projects) both the WebApi and UI when building. Obviously because the data is coming from the WebApi, the WebApi needs to be running in order to access 
the data for the UI views in the CustomerAppliaction.Web, so they both need to be running locally.

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

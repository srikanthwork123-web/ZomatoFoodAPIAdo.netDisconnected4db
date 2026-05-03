using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZomatoFoodAPI_BusinessEntities.Dtos;
using ZomatoFoodAPI_BusinessEntities.Interfaces;
using ZomatoFoodAPI_RepositoryLayer;
using ZomatoFoodAPI_ServiceLayer;

namespace ZomatoFoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        //Dependency Injection is used to develop loosly coupling between the classes.(Don't create object of depency class directly into the controller)
        //(By using interfaces and interface implanted classe we can achieve loosly coupling between the classes.)
        //Depency injection used to avoid tightly coupling between the classes.

        //Dependency injection TYPES we can implement in 3 ways at controller level
        //1.constructor injection(Realtime used constructor injection 99%)
        //2.property injection(not used in real time application development)
        //3.method injection(not used in real time application development)
        //**You can use any one the type .realtime mainly used constructor injection




        #region before dependency injection we are creating the object of the service class in the controller class to access the members of that service class
        //employeeservice obj=new employeeservice();//this called tightly coupling process (Don't create object of depency class directly into controller classes).
        //this process not used in real time application development because it will create tight coupling between the classes and it will create the dependency between the classes and it will create the maintenance issue in the application.
        #endregion

        #region 1.constructor injection process (Realtime used 99%)this process
        //************ClassDependencies we  are providing through the constructor of a class.*************
        //IN the  constructor Injection, inject/pass the dependecies to the constructor 
        //Injection means add your depencies to constructior.
        //all depenceies we are passing/Injecting into to the constructor.
        private readonly IEmployeeService _employeeService;//constructor injection.
        private readonly IFilesUploadService _filesUploadService;
        private readonly IUserService _userService;
        //synatx:private readonly interface interfacerefrencevariable;

        //this process is called constructor injection,a class level dependencies are adding here
        public EmployeeController(IEmployeeService employeeService, IFilesUploadService filesUploadService, IUserService userService)//we are inject the dependency in the constructor of the controller class and then we are assigning that dependency to the private readonly field of the interface type and then we can use that private readonly field to access the members of the service class in the controller class.
        {
            _employeeService = employeeService;//Assiging the interface refence variables here
            _filesUploadService = filesUploadService;
            _userService = userService;
        }

        #endregion

        #region 2.PropertyInjection process(but currently not used in real time application development)
        //In the property injection, the dependency is provided through a public property.(this way also you use)
        //public   IFilesUploadService _filesUploadService { get; set; }
        //private  IUserService _userService { get; set; } 
        //private  IEmployeeService _employeeService { get; set; }
        #endregion

        #region 3.MethodInjection process(but currently not used in real time application development)

        //public IActionResult Index([FromServices] IEmployeeService _employeeService)
        //{
        //    return Ok();
        //}

        #endregion

        [HttpGet]
        [Route("GetEmployee")]
        public async Task<IActionResult> GetEmployees()
        {//if requirement demands we can also featch the data from diffrent services in employee controller and then we can send that data to the client in a single response by using anonymous object or by using tuple or by using any other way to send the data to the client in a single response.
            try
            {
                var empdata = await this._employeeService.GetEmployees();//here we are getting the employee data from the employee service and then we are sending that data to the client in a single response.
                var userlistData = await this._userService.InvokeUsersList();//here we are getting the userlist data from the user service and then we are sending that data to the client in a single response.
                var filesUploadData = await this._filesUploadService.GetFileUploadList();//here we are getting the filesupload data from the filesupload service and then we are sending that data to the client in a single response.
                //here 3 results data we are assiging into object and we are return.
                var response = new
                { //aliasname we can give to the data which we are sending to the client.
                    EmployeeData = empdata,
                    UserListData = userlistData,
                    FilesUploadData = filesUploadData
                };
               
                if (empdata == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Data Not Found");
                }
                else
                {
                    //here we are sending employee data,userlist data and filesupload data to the client in a single response.
                    return StatusCode(StatusCodes.Status201Created, response);//here we are sending employee data,userlist data and filesupload data to the client in a single response.
                   // return StatusCode(StatusCodes.Status200OK, empdata);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }

        }

        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> Post([FromBody] EmployeeDto empdto)
        {//dtos are used to transafer the data purpose used.
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var empdata = await _employeeService.AddEmployes(empdto);
                   
                     return StatusCode(StatusCodes.Status201Created, empdata);//here we are sending only employee data respose
                }
            }
            catch (Exception ex)
            {//if you got any error we are using this statuscode:Status500InternalServerError
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }
        [HttpDelete]
        [Route("DeleteEmployeeByEmpid/{empid}")]
        public async Task<IActionResult> delete(int empid)
        {
            if (empid < 0)
            {//If input parameters are wrongly sent or empty, we will get 400 badrequest statuscode:Status400BadRequest
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }
            try
            {
                var empdata = await _employeeService.DeleteEmployesById(empid);
                if (empdata == null)
                {//in db if you get empty data we need to retrun this statuscode:Status404NotFound
                    return StatusCode(StatusCodes.Status404NotFound, "empdata not  found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, "deleted successfully");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }
     
        [HttpGet]
        [Route("GetEmployeeByEmpid/{empid}")]
        public async Task<IActionResult> Get(int empid)
        {
            if (empid < 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }
            try
            {
                var empdata = await _employeeService.GetEmployeeById(empid);
                return StatusCode(StatusCodes.Status200OK, empdata);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server eror");
            }
        }
        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> put([FromBody] EmployeeDto empdto)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var empdata = await _employeeService.UpdateEmploye(empdto);
                    return StatusCode(StatusCodes.Status200OK, empdata);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }
    }
}

/*
1.What is depency injection and how to implement it in dotnetcore?
========================================================================
A)
1.Dependency injection is a design pattern.
2.which is used to develop the loosly coupling between the classes.
3)To implement loosly coupling between the classes ,by using interfaces and interface implemented classes we can achive the dependency injection.
=================================================================
4)Depenceny injection is avoid the tightly coupling between the classes.
that mens we can not create direct object of the class in another class to access the members of that class. 
5)In old application development we are creating the object of the class in another class to access the members of that class.
6)but in dependency injection we are not creating the object of the class in another class to access the members of that class.
7)these interfaces we are injecting to controller class by using constructor injection.
8)After that in program.cs we have inbulit depencyinjection  container available,that is called builder and in builder that we need to register our dependencies.,
 interfacename and interface  implemented classes Register into builder.Services object using  any one of AddScoped or AddSingleton or AddTransient methods. 
================********************************************************************************************************************************==========================
2)Can you explain Dependency injection TYPES At Controller level?
=============================================
we can implement in 3 ways at controller level
   //1.constructor injection
   //2.property injection
   //3.method injection
//**You can use any one the type .realtime mainly used constructor injection.
====================================================================================
3)What is the constructor injection process?
A)In the constructor injection, inject/pass the dependecies to the constructor,injection means add your depencies to constructior.class level dependencies  adding in constructor.
create private readonly field of the interface type and then we can use that private readonly field to access the members of the service class in the controller class.
======================================================================================================================
4)Can you explain depencyinjection at program.cs level? (or) can you explain depencyinjectionservice life time methods?(or) can you explain service life time methods in depency injection?
can you explain breifly about AddSingleton,AddScoped and AddTransient methods in depency injection?(or) what are the differences between AddSingleton,AddScoped and AddTransient methods in depency injection?
==========================================================================================================================================================================================
A)
    In program.cs we have three types of dependency injection life time methods are there,they are:
    1)AddSingleton
    2)AddScoped
    3)AddTransient
======================
1)AddSingleton method:
Services.Addsingleton() method:
===========================
By using singleton service it created  only one object and it is used for all httpRequests.
that same object is used for all the times.
if you call  same method multiple times also it generates only one object in runtime.
this is called singleton design pattern.
for entire project we are reading the connection string only one time and that connection string information is used for all the times in the repository layer to connect to the database and perform the required crud operations.
in my project we are registering the connection factory class in the dependency injection container of the application using the AddSingleton method because we are reading the connection string only one time and that connection string information is used for all the times in the repository layer to connect to the database and perform the required crud operations.
builder.Services.AddSingleton<IConnectionFactory, ConnectionFactory>();
##########################################################################
2)AddScoped method:
Services.Addscoped() method:
==========================
By using Addscoped service every httprequest new instance/object  is created.
(whether it is get(or) post(or)put(or)delete opertions, each and every request new instance/object is created for  scoped service).
in my project we are registering the repository interfaces and their implementation classes and service interfaces and their implementation classes in the dependency injection container of the application using the AddScoped method because we want to create new instance/object for every http request to access the members of the repository and service classes in the controller class to perform the required operations.
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
###########################################################################
3)AddTransient method:
Services.AddTransient() method:
===============================
If any 3rd party apis comunication in your Api we can go for this  services.
AddTransient() method.

By using Add Transient service each and every request new instance is created.
if you have any subsequent request it will create another object also.
Note:In one api if you call another api  i.e is subsequent request.


 Note:
•	Services.Addscoped and  services.addtransient almost same.
The main difference is if you have any subsequent request(or)3rdparty api calling  in your service go for services.add transient().
if any database crud opertions in your service go for services.addscoped().
 

//if you want to call any 3rd party api urls calling scenario use AddTraniscent();
in my project we are using like below way to register the user repository and user service in the dependency injection container of the application using the AddTransient method because we are calling 3rd party api urls in the user repository class to get the data from that 3rd party api urls and then we are sending that data to the controller class and then we are sending that data to the client in a single response.
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();








*/

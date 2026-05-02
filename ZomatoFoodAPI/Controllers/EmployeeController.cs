using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZomatoFoodAPI_BusinessEntities.Dtos;
using ZomatoFoodAPI_BusinessEntities.Interfaces;

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
        //1.constructor injection(Realtime used 99%)
        //2.property injection
        //3.method injection
        //**You can use any one the type .realtime mainly used constructor injection




        #region before dependency injection we are creating the object of the service class in the controller class to access the members of that service class process
        //employeeservice obj=new employeeservice();//this called tightly coupling process (Don't create object of depency class directly).
        //this process not used in real time application development because it will create tight coupling between the classes and it will create the dependency between the classes and it will create the maintenance issue in the application.
        #endregion

        #region 1.constructor injection process
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

        #region 2.PropertyInjection process
        //In the property injection, the dependency is provided through a public property.(this way also you use)
        //public   IFilesUploadService _filesUploadService { get; set; }
        //private  IUserService _userService { get; set; } 
        //private  IEmployeeService _employeeService { get; set; }
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
2.which is used to develop the loosly coupling between the classes.(Loosly coulping means dont create direct obect of the repository class in the controller class to access the members of that repository class.)
3)To implement loosly coupling between the classes ,by using interfaces and interface implemented classes we can achive the dependency injection.
=================================================================
4)Depenceny injection is avoid the tight coupling between the classes. that mens we can not create direct object of the class in another class to access the members of that class. 
5)In old application development we are creating the object of the class in another class to access the members of that class.
6)but in dependency injection we are not creating the object of the class in another class to access the members of that class.
7)these interfaces we are injecting to controller class by using constructor injection.
8)After that in program.cs we have inbulit depency dency injection  container is there,that is called builder and in that we need to register 
the interface and its implemented classes into the dependency injection container of the application by using AddScoped or AddSingleton or AddTransient method of the builder.Services object.

*/

using ZomatoFoodAPI_BusinessEntities.Interfaces;
using ZomatoFoodAPI_DbContectivity;
using ZomatoFoodAPI_RepositoryLayer;
using ZomatoFoodAPI_ServiceLayer;

//Program.cs is the entry point of the application Here we are configuring/adding/registering the services and  middlewares to the application.
//In this file we are adding/registering the services and the repositories in the dependency injection container of the application and then we are building the application and running it.

//this program.cs is divided into 2 sections.
//===========================================================
//section1:builder is the inbuilt depency injection conatiner.we need to register our all application depencies into our inbuilt depency injection container.
//================================================================================================================================================================
//this conatiner will load your depencies and then it will inject those depencies to the controller class by using constructor injection and then we can use those depencies in the controller class to perform the required operations.
#region inbuilt dependency injection containerSection.
var builder = WebApplication.CreateBuilder(args);//builder is the inbuilt dependency injection container which is used to register the services and the repositories in the dependency injection container of the application and then we are building the application and running it.
//if you run the program,first it will call program.cs and it will load all the depencies into the memory and then it will inject those depencies to the controller class by using constructor injection and then we can use those depencies in the controller class to perform the required operations
// If you want to add any depencencies to your  container. by using builder.services....we can register our dependicies.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();//this is used to load the swagger

//To implement the depency Injection must and stood register the interfacename,interfaceimplemented class here.
//If you are not registered it will throw "System.InvalidOpertionException:Unable to reslove service type" Error
//These interfaces we are injecting into controller constructor,to  implement the loosely coupling between the classes
//*************WE MUST REGISTER THE INTERFACENAME,INTERFACEIMPLEMNTEDCHILDNAME CLASS NAME HERE to implement depency injection**************
//==============************************************************************************


#region AddSingletonServiceMethod
/*
Services.Addsingleton() method:
===========================
By using singleton service it created  only one object and it is used for all httpRequests.
that same object is used for all the times.
if you call  same method multiple times also it generates only one object in runtime.
this is called singleton design pattern.
for entire project we are reading the connection string only one time and that connection string information is used for all the times in the repository layer to connect to the database and perform the required crud operations.
*/
//Syntax for registering:builder.Services.AddSingleton<InterfaceName,InterfaceImplementedClassName>();
//========================================================================================================
builder.Services.AddSingleton<IConnectionFactory, ConnectionFactory>();//<>  we called this as placeholder(or)angle brackets.

#endregion

#region AddScopedService
/*
Services.Addscoped() method:
==========================
By using Addscoped service every httprequest new instance/object  is created.
(whether it is get(or) post(or)put(or)delete opertions, each and every request new instance/object is created for  scoped service).
*/
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();//register the repository interface and its implementation in the dependency injection container of the application using the AddScoped method   builder object.
builder.Services.AddScoped<IEmployeeService, EmployeeService>();//register the service interface and its implementation in the dependency injection container of the application using the AddScoped method   builder object.

builder.Services.AddScoped<IFilesUploadService, FilesUploadService>();
builder.Services.AddScoped<IFilesUploadRepository, FilesUploadRepository>();
#endregion

#region  AddTransientService
/*
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
 
 */
//if you want to call any 3rd party api urls calling scenario use AddTraniscent();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
#endregion


//===================***********************************************************************
//Here we are adding/Registering the automapper in the dependency injection container of the application using the AddAutoMapper method of the builder.Services object and we are passing the assemblies of the application to the AddAutoMapper method to scan the profiles of automapper in those assemblies and then we can use the automapper in our application to map the entity class objects to dto class objects and vice versa.
//if you are not write this line here,our automapper functionality will not work
#region AutoMapper Adding To DependencyInjection Container
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion

#endregion


//section2:app is the inbuilt request pipeline,heare we need to register our middlewares to application pipeline.
//===================================================================================================================
#region inbuilt request pipelineSection
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion
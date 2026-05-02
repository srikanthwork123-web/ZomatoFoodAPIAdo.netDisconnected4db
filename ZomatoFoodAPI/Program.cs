using ZomatoFoodAPI_BusinessEntities.Interfaces;
using ZomatoFoodAPI_DbContectivity;
using ZomatoFoodAPI_RepositoryLayer;
using ZomatoFoodAPI_ServiceLayer;

//Program.cs is the entry point of the application Here we are configuring/adding/registering the services and  middlewares to the application.
//In this file we are adding/registering the services and the repositories in the dependency injection container of the application and then we are building the application and running it.
//this program.cs is divided into 2 sections.
//section1:builder is the inbuilt depency injection conatiner.we need to register our all application depencies into our inbuilt depency injection container.
//this conatiner will load your depencies and then it will inject those depencies to the controller class by using constructor injection and then we can use those depencies in the controller class to perform the required operations.
#region inbuilt dependency injection containerSection
var builder = WebApplication.CreateBuilder(args);

// If you want to add any depencencies to your  container. by using builder.services....we can register our dependicies.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();//this is used to load the swagger
//==============************************************************************************
builder.Services.AddSingleton<IConnectionFactory, ConnectionFactory>();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();//register the repository interface and its implementation in the dependency injection container of the application using the AddScoped method   builder object.
builder.Services.AddScoped<IEmployeeService, EmployeeService>();//register the service interface and its implementation in the dependency injection container of the application using the AddScoped method   builder object.

builder.Services.AddScoped<IFilesUploadService, FilesUploadService>();
builder.Services.AddScoped<IFilesUploadRepository, FilesUploadRepository>();

//if you want to call any 3rd party api urls calling scenario use AddTraniscent();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

//===================***********************************************************************
//Here we are adding/Registering the automapper in the dependency injection container of the application using the AddAutoMapper method of the builder.Services object and we are passing the assemblies of the application to the AddAutoMapper method to scan the profiles of automapper in those assemblies and then we can use the automapper in our application to map the entity class objects to dto class objects and vice versa.
//if you are not write this line here,our automapper functionality will not work
#region AutoMapper Adding To DependencyInjection Container
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion

#endregion


//section2:app is the inbuilt request pipeline,heare we need to register our middlewares to application pipeline.

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
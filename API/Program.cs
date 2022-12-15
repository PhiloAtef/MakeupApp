// we use the builder config that creates our web application instance. effectively, this allows us to run the application.
// it looks inside the file and executes the code in program.cs

using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//using the entity framework by providing the datacontext as a service
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")); //needs dependency injection
});

// SWagger/Open AI (gonna be replaced by postman)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build(); //the section before this is considered the services container

// Configure the HTTP request pipeline.

// we can add authorization for http requests here
if (app.Environment.IsDevelopment()) //flag for developer mode
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers(); //the middleware that tells our request which API endpoint it needs to go to or the controller for that endpoint

app.Run(); //the command to actually run our application 

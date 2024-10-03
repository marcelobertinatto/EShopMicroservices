using Order.API;
using Order.Application;
using Order.Infra;
using Order.Infra.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Infra - EF Core
//Application - MediatR
//API - Carter, HealthChecks
builder.Services
    .AddApplicationServices()
    .AddInfraServices(builder.Configuration)
    .AddApiServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseApiServices();

if(app.Environment.IsDevelopment()) 
{
    await app.InitialiseDatabaseAsync();
}

app.Run();

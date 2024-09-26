using Order.API;
using Order.Application;
using Order.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Infra - EF Core
//Application - MediatR
//API - Carter, HealthChecks
builder.Services
    .AddApplicationServices()
    .AddInfraServices(builder.Configuration)
    .AddApiServices();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.Run();

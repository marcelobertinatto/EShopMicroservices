using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.RateLimiting;
using Yarp.ReverseProxy.Configuration;

var builder = WebApplication.CreateBuilder(args);

//add services to the container
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddRateLimiter(rateLimiterOptions => 
{
    rateLimiterOptions.AddFixedWindowLimiter("fixed", options => 
    {
        options.PermitLimit = 5;
        options.Window = TimeSpan.FromSeconds(10);        
        options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 0;
    });
});

var app = builder.Build();

//Configure HTTP Request Pipeline
app.UseRateLimiter();

app.MapReverseProxy();

app.Run();

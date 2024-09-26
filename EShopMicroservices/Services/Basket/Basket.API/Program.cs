using Discount.Grpc;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container

//Application Services
var assembly = typeof(Program).Assembly;

//Carter Lib
builder.Services.AddCarter();
//MediatR lib
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    //applied only for commands
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    //applied only for queries
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

//Data Services
//Marten Lib
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    //setting UserName as an identity property
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

//Repository
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
//Scrutor lib to mapping DI
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();
//Stack Exchange Redis Cache
builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = builder.Configuration.GetConnectionString("Redis");
    //opt.InstanceName = "Basket";
});

//gRPC Services
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>( opt =>
{
    opt.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
}).ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler{
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };

    return handler;
});

//Cross-Cutting Services
//custom exception handler for any 
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//Health Check
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);




var app = builder.Build();

//Configure the HTTP request pipeline.

//Carter
app.MapCarter();

//Custom Exception Handler
app.UseExceptionHandler(opt => { });

//Health Check
app.UseHealthChecks("/health", 
    new HealthCheckOptions 
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();


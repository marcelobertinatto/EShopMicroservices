using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container

//Repository
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

//add Carter
builder.Services.AddCarter();

var assembly = typeof(Program).Assembly;

//MediatR lib
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    //applied only for commands
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    //applied only for queries
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

//Marten Lib
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    //setting UserName as an identity property
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

//custom exception handler for any 
builder.Services.AddExceptionHandler<CustomExceptionHandler>();


var app = builder.Build();

//Configure the HTTP request pipeline.

//Carter
app.MapCarter();

app.UseExceptionHandler(opt => { });

app.Run();


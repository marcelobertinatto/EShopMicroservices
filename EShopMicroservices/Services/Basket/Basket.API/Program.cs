var builder = WebApplication.CreateBuilder(args);

//Add services to the container

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


var app = builder.Build();

//Configure the HTTP request pipeline.

//Carter
app.MapCarter();

app.UseExceptionHandler(opt => { });

app.Run();


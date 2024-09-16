

var builder = WebApplication.CreateBuilder(args);

//Add services to the container

//swagger
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Description = "Carter Sample API",
//        Version = "v1",
//        Title = "Microservices API"
//    });
//});

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

//FluentValidation
builder.Services.AddValidatorsFromAssembly(assembly);

//Carter Lib
builder.Services.AddCarter();

//Marten Lib
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

//Configure the HTTP request pipeline.

//middleware to handle exceptions
//app.UseMiddleware<ValidationExceptionHandlingMiddleware>();

//Carter
app.MapCarter();

app.UseExceptionHandler(opt => { });

//swagger
//app.UseSwagger();
//app.UseSwaggerUI(c =>
//{
//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EshopMicroservices v1");
//});

//app.UseHttpsRedirection();


app.Run();

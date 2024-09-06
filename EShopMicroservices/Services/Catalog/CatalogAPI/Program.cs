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

//Carter Lib
builder.Services.AddCarter();

//MediatR lib
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

//Marten Lib
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();

//Configure the HTTP request pipeline.

//Carter
app.MapCarter();

//swagger
//app.UseSwagger();
//app.UseSwaggerUI(c =>
//{
//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EshopMicroservices v1");
//});

//app.UseHttpsRedirection();


app.Run();

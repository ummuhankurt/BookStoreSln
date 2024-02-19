using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection; // Servisleri eklemeyi unutmayýn
using System.Reflection;
using WebApi.DbOperations;
using WebApi.Middlewares;
using WebApi.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase(databaseName : "BookStoreDB"));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<ILoggerService, DbLogger>(); 
var app = builder.Build();
//DataGenerator.Initialize(app.Services);

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<BookStoreDbContext>();
        DataGenerator.Initialize(services);
    }
    catch (Exception ex)
    {
        // Handle any exceptions here
    }
}
// Configure the HTTP request pipeline. 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCustomExceptionMiddleware();



app.MapControllers();

app.Run();

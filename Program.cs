using EStoreAPI.Data;
using EStoreAPI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "EstoreAPI",
        Version = "v1",
        Description = "Ett ASP.NET Core API",
        Contact = new OpenApiContact
        {
            Name = "Sofia Nordström",
            Email = "sofia.k.nordstrom@gmail.com",
            Url = new Uri("https://github.com/SofiaNords/EStoreAPI")
        }, 
        //License = new OpenApiLicense
        //{
        //    Name = "MIT License",
        //    Url = new Uri("https://opensource.org/licenses/MIT")
        //}
    });
});

builder.Services.AddSingleton<MongoDbService>();

builder.Services.AddScoped<IMongoDatabase>(serviceProvider =>
{
    var mongoDbService = serviceProvider.GetRequiredService<MongoDbService>();
    return mongoDbService.Database;
});

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

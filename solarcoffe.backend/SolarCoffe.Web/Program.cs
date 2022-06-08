using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using SolarCoffe.Data;
using SolarCoffe.Services.Customer.Interfaces;
using SolarCoffe.Services.Customer.Services;
using SolarCoffe.Services.Inventory.Interfaces;
using SolarCoffe.Services.Inventory.Services;
using SolarCoffe.Services.Order.Interfaces;
using SolarCoffe.Services.Order.Services;
using SolarCoffe.Services.Product.Interfaces;
using SolarCoffe.Services.Product.Services;
using SolarCoffe.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();

builder.Services.AddControllers().AddNewtonsoftJson(opts =>
{
    opts.SerializerSettings.ContractResolver = new DefaultContractResolver
    {
        NamingStrategy = new CamelCaseNamingStrategy()
    };
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SolarDbContext>(opts =>
{
    opts.EnableDetailedErrors();
    opts.UseNpgsql(builder.Configuration.GetConnectionString("solardev"));
});
builder.Services.AddAutoMapperConfig();

builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ICostumerService, CustomerService>();
builder.Services.AddTransient<IInventoryService, InventoryService>();
builder.Services.AddTransient<IOrderService, OrderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(builder => builder.AllowAnyHeader()
                            .AllowAnyMethod()
                            // .AllowCredentials()
                            .WithOrigins("http://localhost:8080"));

app.UseAuthorization();

app.MapControllers();

app.Run();

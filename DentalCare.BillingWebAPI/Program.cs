using DentalCare.BLL;
using DentalCare.BLL.Interface;
using DentalCare.DAL.Interface;
using DentalCare.DAL.Repository;
using DentalCare.BillingWebAPI.Extensions;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging();
builder.Services.AddJWTTokenServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddScoped<IBillingBLL, BillingBLL>();

var connectionString = builder.Configuration.GetConnectionString("Local");
builder.Services.AddScoped<IDentalCareRepository>(s => new DentalCareRepository(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
}

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();


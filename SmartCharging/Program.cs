using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SmartCharging.API.Data;
using SmartCharging.API.Data.Repository.Contracts;
using SmartCharging.API.Data.Repository.Repos;
using SmartCharging.API.Domain.Contracts;
using SmartCharging.API.Domain.Services;
using SmartCharging.API.ViewModels;
using SmartCharging.API.ViewModelServices;
using SmartCharging.API.ViewModelServices.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SmartChargingDbContext>(options =>
    options.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = SmartChargingDataBase"));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services and repositories.
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<ISmartGroupsRepository, SmartGroupsRepository>();
builder.Services.AddScoped<IChargeStationRepository, ChargeStationRepository>();
builder.Services.AddScoped<ISmartGroupServices, SmartGroupServices>();
builder.Services.AddScoped<IChargeStationServices, ChargeStationServices>();
builder.Services.AddScoped<IConnectorServices, ConnectorServices>();
builder.Services.AddScoped<ISmartGroupViewModelService, SmartGroupViewModelService>();
builder.Services.AddScoped<IChargeStationViewModelService, ChargeStationViewModelService>();

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

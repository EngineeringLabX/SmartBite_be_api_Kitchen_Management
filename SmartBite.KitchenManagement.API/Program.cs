using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using SmartBite.KitchenManagement.Application.Interfaces;
using SmartBite.KitchenManagement.Application.Mappers;
using SmartBite.KitchenManagement.Application.Services;
using SmartBite.KitchenManagement.Domain.Interfaces;
using SmartBite.KitchenManagement.Infrastructure.Persistance;
using SmartBite.KitchenManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<KitchenDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KitchenManagementDbConnection")));

builder.Services.AddAutoMapper(cfg => { }, typeof(KitchenProfile).Assembly);

builder.Services.AddScoped<IKitchenRepository, KitchenRepository>();

builder.Services.AddScoped<IKitchenService, KitchenService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

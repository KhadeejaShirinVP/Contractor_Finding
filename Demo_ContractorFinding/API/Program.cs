using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Persistence;
using Service.Interfaces;
using Service;
using Repository;
using AutoMapper;
using Domain.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllersWithViews().AddNewtonsoftJson
    (options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson
    (options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
var connectionString = builder.Configuration.GetConnectionString("dbcon");
builder.Services.AddDbContext<ContractorFindingDemoContext>(option =>
option.UseSqlServer(connectionString)
);
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEncrypt, Encrypt>();
builder.Services.AddScoped<IContractorService, ContractorService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ISendMessage,SendMessage>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

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

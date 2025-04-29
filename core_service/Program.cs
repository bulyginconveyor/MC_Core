using core_service.application.middleware;
using core_service.domain;
using core_service.domain.logic;
using core_service.domain.models;
using core_service.infrastructure.repository.interfaces;
using core_service.infrastructure.repository.postgresql;
using core_service.infrastructure.repository.postgresql.context;
using core_service.infrastructure.repository.postgresql.repositories.@base;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    Env.Load("dev.env");
}

// Add service DbContext
builder.Services.AddPostgreSqlDbContext();

// Add services with Repository
builder.Services.AddRepositories();

// Add services with Logic
builder.Services.AddLogics();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();
//builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.UseErrorBoundary();

app.Run();

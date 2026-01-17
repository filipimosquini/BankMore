using Account.Api.Configurations;
using Account.Api.Configurations.Extensions;
using Account.Api.Configurations.Middlewares;
using Account.Infrastructure.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

Console.WriteLine(builder.Environment.EnvironmentName);
// Add services to the container.

builder.Services.AddControllers();

// Configurations
builder.Services
    .AddDbContext(builder.Configuration)
    .AddingCors()
    .AddingResponseCompression()
    .AddingAuthentication(builder.Configuration)
    .AddAuthorization();

// Project
builder.Services
    .AddResourcesDependencies()
    .AddServicesDependencies()
    .AddValidatorDependencies()
    .AddMediatrDependencies();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwaggerConfiguration();

app.UseMiddleware<ExceptionMiddleware>();

if (builder.Environment.IsProduction())
{
    app.MigrateDatabase();
}

app.UseResponseCompression();
app.UseHttpsRedirection();

app.UseRouting();
app.UseCors(x =>
    x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

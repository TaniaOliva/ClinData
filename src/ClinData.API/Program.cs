using ClinData.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using ClinData.Application.DependencyInjection;
using ClinData.Infrastructure.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// agregar servicios al contenedor.

builder.Services.AddControllers();
builder.Services.AddDbContext<ClinDataDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClinData")));
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "ClinData API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
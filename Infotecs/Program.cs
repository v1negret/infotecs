using Infotecs.Data;
using Infotecs.Repositories;
using Infotecs.Repositories.Interfaces;
using Infotecs.Services;
using Infotecs.Services.Interfaces;
using Infotecs.Shared;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseNpgsql(config.GetConnectionString("DefaultConnection"));
});

builder.Services.AddLocalization();
builder.Services.AddScoped<ICsvService, CsvService>();
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<ITimescaleDataAggregator, TimescaleDataAggregator>();
builder.Services.AddScoped<IFileService, FileService>();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/openapi/v1.json", "infotecs test api v.1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
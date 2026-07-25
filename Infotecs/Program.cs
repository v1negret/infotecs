using Infotecs.Data;
using Infotecs.Repositories;
using Infotecs.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseNpgsql(config.GetConnectionString("DefaultConnection"));
});

builder.Services.AddLocalization(opt => opt.ResourcesPath = "Shared");
builder.Services.AddScoped<CsvService>();
builder.Services.AddScoped<FileRepository>();
builder.Services.AddScoped<TimescaleDataAggregator>();
builder.Services.AddScoped<FileService>();

var app = builder.Build();

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